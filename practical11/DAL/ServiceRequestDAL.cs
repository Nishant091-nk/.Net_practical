using SmartCampusPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartCampusPortal.DAL
{
    public class ServiceRequestDAL
    {
        private readonly string cs =
           ConfigurationManager.ConnectionStrings[
               "CampusConnection"].ConnectionString;

        // Get all requests
        public List<ServiceRequest> GetAll()
        {
            List<ServiceRequest> list =
                new List<ServiceRequest>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM ServiceRequests";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new ServiceRequest
                    {
                        RequestId = Convert.ToInt32(
                            dr["RequestId"]),

                        StudentName = dr["StudentName"].ToString(),

                        Department = dr["Department"].ToString(),

                        ServiceType = dr["ServiceType"].ToString(),

                        Description = dr["Description"].ToString(),

                        Priority = dr["Priority"].ToString(),

                        Status = dr["Status"].ToString(),

                        RequestDate = Convert.ToDateTime(
                            dr["RequestDate"])
                    });
                }
            }

            return list;
        }

        // Insert new request
        public void Insert(ServiceRequest request)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    INSERT INTO ServiceRequests
                    (StudentName, Department, ServiceType,
                     Description, Priority, Status)
                    VALUES
                    (@StudentName, @Department, @ServiceType,
                     @Description, @Priority, @Status)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@StudentName", request.StudentName);

                cmd.Parameters.AddWithValue(
                    "@Department", request.Department);

                cmd.Parameters.AddWithValue(
                    "@ServiceType", request.ServiceType);

                cmd.Parameters.AddWithValue(
                    "@Description", request.Description);

                cmd.Parameters.AddWithValue(
                    "@Priority", request.Priority);

                cmd.Parameters.AddWithValue(
                    "@Status", "Pending");

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        // Update request status
        public void UpdateStatus(int id, string status)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    UPDATE ServiceRequests
                    SET Status = @Status
                    WHERE RequestId = @RequestId";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Status", status);

                cmd.Parameters.AddWithValue("@RequestId", id);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}