﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataInMemory
{
    public class InMemoryRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;

        List<T> items;
        string ClassName;

        public InMemoryRepository(){

            ClassName = typeof(T).Name;
            items = cache[ClassName] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit() {
            cache[ClassName] = items;
        }

        public void Insert(T t) {

            items.Add(t);
        }

        public void UpDate(T t) {

            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else 
            {
                throw new Exception(ClassName + "Not Found!");
            }
        }

        public  T Find(string Id) {

            T t = items.Find(i => i.Id == Id);
            if (t != null) {
                return t;
            }
            else
            {
                throw new Exception(ClassName + "Not Found!");
            }
        }

        public IQueryable<T> Collection() {

            return items.AsQueryable();
        }

        public void Delete(string Id) {

            T tToDelete = items.Find(i => i.Id == Id);
            if(tToDelete != null) {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(ClassName + "Not Found!");
            }
        }
    }
}
