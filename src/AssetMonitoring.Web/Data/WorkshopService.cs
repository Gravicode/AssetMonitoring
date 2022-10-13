﻿using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class WorkshopService : ICrud<Workshop>
    {
        AssetMonitoringDB db;

        public WorkshopService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Workshops.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Workshops.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Workshop> FindByKeyword(string Keyword)
        {
            var data = from x in db.Workshops
                       where x.Nama.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Workshop> GetAllData()
        {
            return db.Workshops.ToList();
        }

        public Workshop GetDataById(object Id)
        {
            return db.Workshops.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Workshop data)
        {
            try
            {
                db.Workshops.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Workshop data)
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
            return db.Workshops.Max(x => x.Id);
        }
    }

}