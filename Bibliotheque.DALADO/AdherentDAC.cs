using Bibliotheque.BOL;
using Bibliotheque.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Bibliotheque.DALADO
{
    
    

    public class AdherentADO : IAdherentRepository
    {
        private static AdherentADO _instance = null;
        private static object _verrou = new object();
        public static AdherentADO Instance
        {
            get
            {
                lock (_verrou)
                {
                    if (_instance == null)
                    {
                        _instance = new AdherentADO();
                    }
                }
                return _instance;
            }
        }


        #region IAdherentRepository Membres

        public IList<Adherent> ListerAdherents()
        {
            IList<Adherent> adherents = null;
            using (DbConnection cnx = DBDAO.Instance.GetDBConnection())
            using (DbCommand cmd = cnx.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Adherent";
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        if (adherents == null) adherents = new List<Adherent>();
                        Adherent adherent = new Adherent();
                        adherent.IdAdherent = rd["IdAdherent"].ToString();
                        adherent.NomAdherent = rd["NomAdherent"].ToString();
                        adherent.PrenomAdherent = rd["PrenomAdherent"] ==
                            DBNull.Value ? string.Empty : rd["PrenomAdherent"].ToString();
                        if (adherents == null) adherents = new List<Adherent>();
                        adherents.Add(adherent);
                    }
                }
            }
            return adherents;
        }

        public Adherent SelectionnerAdherentByID(string IdAdherent)
        {
            Adherent adherent = null;
            using (DbConnection cnx = DBDAO.Instance.GetDBConnection())
            using (DbCommand cmd = cnx.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                DbParameter pIdAdherent = cmd.CreateParameter();
                pIdAdherent.ParameterName = "@IdAdherent";
                pIdAdherent.DbType = DbType.String;
                pIdAdherent.Direction = ParameterDirection.InputOutput;
                pIdAdherent.Value = IdAdherent;
                cmd.Parameters.Add(pIdAdherent);
                cmd.CommandText = "Select * from Adherent where idAdherent = @IdAdherent";
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        adherent = new Adherent();
                        adherent.IdAdherent = rd["IdAdherent"].ToString();
                        adherent.NomAdherent = rd["NomAdherent"].ToString();
                        adherent.PrenomAdherent = rd["PrenomAdherent"] ==
                            DBNull.Value ? string.Empty : rd["PrenomAdherent"].ToString();
                    }
                }
            }
            return adherent;
        }

        public Adherent AjouterAdherent(Adherent nouvelAdherent)
        {

            Adherent adherent = null;
            return adherent;
        }

        #endregion
    }

}
