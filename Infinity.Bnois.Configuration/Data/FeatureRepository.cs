
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infinity.Bnois.Configuration.Data
{
    public class FeatureRepository : CompanyConfigRepository<Feature>, IFeatureRepository
    {
        internal SqlConnection Connection;
        public FeatureRepository(ConfigurationDbContext context) : base(context)
        {
            Connection = context.Database.Connection as SqlConnection;
        }

        public DataTable ExecWithSqlQuery(string query)
        {
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if
                    (Connection.State == ConnectionState.Open)
                {

                    Connection.Close();
                }
            }
        }

        public List<Feature> GetAllFeatures(int pageSize, int pageNumber, string catagory, string searchText, out int total)
        {
            IQueryable<Feature> featureQuery = context.Features.AsQueryable();

            if (!string.IsNullOrWhiteSpace(catagory) && !string.IsNullOrWhiteSpace(searchText))
            {

            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                featureQuery = featureQuery.Where(x => x.FeatureName.Contains(searchText));
                total = featureQuery.Count();
                return featureQuery.OrderByDescending(x => x.FeatureName).Skip((pageNumber - 1)*pageSize).Take(pageSize).ToList();
            }

            total = featureQuery.Count();
            return featureQuery.OrderByDescending(x => x.FeatureName).Skip((pageNumber - 1)*pageSize).Take(pageSize).ToList();
        }
    }
}