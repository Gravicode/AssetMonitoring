using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class WarehouseService : ICrud<Warehouse>
    {
        AssetMonitoringDB db;

        public WarehouseService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Warehouses.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Warehouses.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Warehouse> FindByKeyword(string Keyword)
        {
            var data = from x in db.Warehouses
                       where x.Nama.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Warehouse> GetAllData()
        {
            return db.Warehouses.ToList();
        }

        public Warehouse GetDataById(object Id)
        {
            return db.Warehouses.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Warehouse data)
        {
            try
            {
                db.Warehouses.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Warehouse data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.Warehouses.Max(x => x.Id);
        }
    }

}