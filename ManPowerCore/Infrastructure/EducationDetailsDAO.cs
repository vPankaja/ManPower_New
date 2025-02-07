﻿using ManPowerCore.Common;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManPowerCore.Infrastructure
{
    public interface EducationDetailsDAO
    {
        int SaveEducationDetails(EducationDetails educationDetails, DBConnection dbConnection);

        List<EducationDetails> GetAllEducationDetails(DBConnection dbConnection);

        EducationDetails GetEducationDetailsById(int id, DBConnection dbConnection);

        int UpdateEducationDetails(EducationDetails educationDetails, DBConnection dbConnection);

        List<EducationDetails> GetEducationDetailsByEmpId(int empId, DBConnection dbConnection);
    }

    public class EducationDetailsDAOImpl : EducationDetailsDAO
    {
        public int SaveEducationDetails(EducationDetails educationDetails, DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO EDUCATION_DETAILS(EMPLOYEE_ID,EDUCATION_TYPE_ID,INSTITUTE,ATTEMPT,YEAR,INDEX_NO,SUBJECT,STREAM,GRADE,STATUS) " +

                                            "VALUES(@EmployeId,@EduTypeId,@Institute,@Attempts,@Year,@Index,@Subject,@Stream,@Grade,@Status) ";



            dbConnection.cmd.Parameters.AddWithValue("@EmployeId", educationDetails.EmployeeId);
            dbConnection.cmd.Parameters.AddWithValue("@EduTypeId", educationDetails.EducationTypeId);
            dbConnection.cmd.Parameters.AddWithValue("@Institute", educationDetails.StudiedInstitute);
            dbConnection.cmd.Parameters.AddWithValue("@Index", educationDetails.ExamIndex);
            dbConnection.cmd.Parameters.AddWithValue("@Attempts", educationDetails.NoOfAttempts);
            dbConnection.cmd.Parameters.AddWithValue("@Year", educationDetails.ExamYear);
            dbConnection.cmd.Parameters.AddWithValue("@Subject", educationDetails.ExamSubject);
            dbConnection.cmd.Parameters.AddWithValue("@Stream", educationDetails.ExamStream);
            dbConnection.cmd.Parameters.AddWithValue("@Grade", educationDetails.ExamGrade);
            dbConnection.cmd.Parameters.AddWithValue("@Status", educationDetails.ExamStatus);


            dbConnection.cmd.ExecuteNonQuery();
            dbConnection.cmd.Parameters.Clear();
            return 1;
        }

        public int UpdateEducationDetails(EducationDetails educationDetails, DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE EDUCATION_DETAILS SET EDUCATION_TYPE_ID = @EduTypeId,INSTITUTE = @Institute" +
                                           ",ATTEMPT = @Attempts,YEAR = @Year ,INDEX_NO = @Index ,SUBJECT = @Subject" +
                                           ",STREAM = @Stream ,GRADE = @Grade ,STATUS = @Status WHERE ID = @EducationDetailsId";

            dbConnection.cmd.Parameters.AddWithValue("@EmployeId", educationDetails.EmployeeId);
            dbConnection.cmd.Parameters.AddWithValue("@EduTypeId", educationDetails.EducationTypeId);
            dbConnection.cmd.Parameters.AddWithValue("@Institute", educationDetails.StudiedInstitute);
            dbConnection.cmd.Parameters.AddWithValue("@Index", educationDetails.ExamIndex);
            dbConnection.cmd.Parameters.AddWithValue("@Attempts", educationDetails.NoOfAttempts);
            dbConnection.cmd.Parameters.AddWithValue("@Year", educationDetails.ExamYear);
            dbConnection.cmd.Parameters.AddWithValue("@Subject", educationDetails.ExamSubject);
            dbConnection.cmd.Parameters.AddWithValue("@Stream", educationDetails.ExamStream);
            dbConnection.cmd.Parameters.AddWithValue("@Grade", educationDetails.ExamGrade);
            dbConnection.cmd.Parameters.AddWithValue("@Status", educationDetails.ExamStatus);
            dbConnection.cmd.Parameters.AddWithValue("@EducationDetailsId", educationDetails.EducationDetailsId);


            dbConnection.cmd.ExecuteNonQuery();
            dbConnection.cmd.Parameters.Clear();
            return 1;
        }

        public List<EducationDetails> GetAllEducationDetails(DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandText = "SELECT * FROM EDUCATION_DETAILS ";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            return dataAccessObject.ReadCollection<EducationDetails>(dbConnection.dr);

        }

        public EducationDetails GetEducationDetailsById(int id, DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandText = "SELECT * FROM EDUCATION_DETAILS WHERE ID=" + id + " ";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            return dataAccessObject.GetSingleOject<EducationDetails>(dbConnection.dr);
        }

        public List<EducationDetails> GetEducationDetailsByEmpId(int empId, DBConnection dbConnection)
        {
            if (dbConnection.dr != null)
                dbConnection.dr.Close();

            dbConnection.cmd.CommandText = "SELECT * FROM EDUCATION_DETAILS WHERE EMPLOYEE_ID=" + empId + " ";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            return dataAccessObject.ReadCollection<EducationDetails>(dbConnection.dr);
        }
    }
}
