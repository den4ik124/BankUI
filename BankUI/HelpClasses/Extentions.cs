using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using DataProcessorLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BankUI.HelpClasses
{
    internal static class Extentions
    {
        private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        };

        private static IDataProcessor _dataProcessor = new DataProcessor();
        private static IDialogService _dialogService = new DialogService();

        public static void AddClientToDB(this ClientModel client)
        {
            if (!ClientsDBModel.Clients.Contains(client))
            {
                ClientsDBModel.Clients.Add(client);
                foreach (var account in client.AccountsList)
                    account.AddAccountToDB();
                ClientsDBModel.UpdateClients();
            }
        }

        public static void RemoveClientFromDB(this ClientModel client)
        {
            ClientsDBModel.Clients.Remove(client);
            ClientsDBModel.UpdateClients();
        }

        public static void RemoveAccountFromDB(this IAccount account)
        {
            foreach (var client in ClientsDBModel.Clients)
                foreach (var acc in client.AccountsList)
                    if (account.HostId == acc.HostId)
                    {
                        client.AccountsList.Remove(account);
                        client.TotalBalanceCalc();
                    }
            AccountsDBModel.Accounts.Remove(account);
            account.Save(AccountsDBModel.FileName);
            //AccountsDBModel.SaveDB();
        }

        public static void AddAccountToDB(this IAccount account)
        {
            if (!AccountsDBModel.Accounts.Contains(account))
            {
                AccountsDBModel.Accounts.Add(account);
                account.Save(AccountsDBModel.FileName);
                //AccountsDBModel.SaveDB();
            }
        }

        public static void Save<T>(this T toSerializeObject, string path = "")
        {
            _dataProcessor.Serialization(toSerializeObject, path);
        }
    }
}