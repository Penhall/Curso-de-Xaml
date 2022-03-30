using EvernoteClone.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace EvernoteClone.ViewModel.Helper
{
    public class SQLiteHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();

                int rows = connection.Insert(item);
                if (rows > 0)
                    result = true;
            }

            return result;
        }
        public static bool Delete<T>(T item) where T : IHasId
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();

                int rows = connection.Delete(item);
                if (rows > 0)
                    result = true;
            }

            return result;


        }
        public static bool Update<T>(T item) where T : IHasId
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();

                int rows = connection.Update(item);
                if (rows > 0)
                    result = true;
            }

            return result;


        }
        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            }

            return items;

        }


    }
}

