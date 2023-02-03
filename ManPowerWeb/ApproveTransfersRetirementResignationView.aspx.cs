﻿using ManPowerCore.Common;
using ManPowerCore.Controller;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManPowerWeb
{
    public partial class ApproveTransfersRetirementResignationView : System.Web.UI.Page
    {
        static int Id;
        static int typeId;
        static string document;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]);
                    BindData();
                    BindStatus();
                }
                else
                {
                    Response.Redirect("404.aspx");
                }
            }
        }

        private void BindData()
        {
            TransfersRetirementResignationMainController trrController = ControllerFactory.CreateTransfersRetirementResignationMainController();
            TransfersRetirementResignationMain trrmainObj = trrController.GetTransfersRetirementResignation(Id);

            lblEmpNumber.Text = trrmainObj.EmployeeId.ToString();
            lblEmpName.Text = trrmainObj.employee.EmpInitials + trrmainObj.employee.LastName;

            SystemUserController systemUserController = ControllerFactory.CreateSystemUserController();
            SystemUser systemUser = systemUserController.GetSystemUserByEmpNumber(trrmainObj.EmployeeId);

            DepartmentUnitController departmentUnitController = ControllerFactory.CreateDepartmentUnitController();
            DepartmentUnit departmentUnit = departmentUnitController.GetDepartmentUnit(systemUser._DepartmentUnitPositions.DepartmentUnitId, true, false);

            lblDepartment.Text = departmentUnit.Name;
            lblDesignation.Text = systemUser._Designation.DesigntionName;
            lblDocument.Text = trrmainObj.Documents;
            document = trrmainObj.Documents;

            if (trrmainObj.RequestTypeId == 1)
            {
                typeId = 1;
                transferDiv.Visible = true;
                heading.Text = "Transfer - " + trrmainObj.EmployeeId;
                lblRequestType.Text = "Transfer";

                TransferController transferController = ControllerFactory.CreateTransferController();
                Transfer transfer = transferController.GetTransferByMainId(Id);

                lblTransferType.Text = transfer.TransferType;
                lblTransferReason.Text = transfer.Reason;

                DepartmentUnit departmentUnitNext = departmentUnitController.GetDepartmentUnit(transfer.NextDep, false, false);
                lblNewDapartment.Text = departmentUnitNext.Name;
            }
            if (trrmainObj.RequestTypeId == 2)
            {
                typeId = 2;
                resignationDiv.Visible = true;
                heading.Text = "Resignation - " + trrmainObj.EmployeeId;
                lblRequestType.Text = "Resignation";

                ResignationController resignationController = ControllerFactory.CreateResignationController();
                Resignation resignation = resignationController.GetResignationByMainId(Id);

                lblResignationDate.Text = resignation.ResignationDate.ToShortDateString();
                lblResignationReason.Text = resignation.Reason;
            }
            if (trrmainObj.RequestTypeId == 3)
            {
                typeId = 3;
                retirementDiv.Visible = true;
                heading.Text = "Retirement - " + trrmainObj.EmployeeId;
                lblRequestType.Text = "Retirement";

                RetirementController retirementController = ControllerFactory.CreateRetirementController();
                Retirement retirement = retirementController.GetRetirementByMainId(Id);

                EmployeeController employeeController = ControllerFactory.CreateEmployeeController();
                Employee employee = employeeController.GetEmployeeById(trrmainObj.EmployeeId);

                lblJoinedDate.Text = retirement.JoinedDate.ToShortDateString();
                lblDob.Text = employee.DOB.ToShortDateString();
                lblRetirementType.Text = retirement.RetirementType;
                lblRetirementReason.Text = retirement.Reason;
                lblRetirementRemark.Text = retirement.Remark;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApproveTransfersRetirementResignation.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (typeId == 1)
            {
                if (document != "" && document != null)
                {
                    string filePathe = Server.MapPath("~/SystemDocuments/Transfers/" + document);

                    Response.Clear();
                    Response.ContentType = "application/octect-stream";
                    Response.AppendHeader("content-disposition", "filename = " + document);
                    Response.TransmitFile(filePathe);
                    Response.End();
                }
            }
            if (typeId == 2)
            {
                if (document != "" && document != null)
                {
                    string filePathe = Server.MapPath("~/SystemDocuments/Resignation/" + document);

                    Response.Clear();
                    Response.ContentType = "application/octect-stream";
                    Response.AppendHeader("content-disposition", "filename = " + document);
                    Response.TransmitFile(filePathe);
                    Response.End();
                }
            }
            if (typeId == 3)
            {
                if (document != "" && document != null)
                {
                    string filePathe = Server.MapPath("~/SystemDocuments/Retirement/" + document);

                    Response.Clear();
                    Response.ContentType = "application/octect-stream";
                    Response.AppendHeader("content-disposition", "filename = " + document);
                    Response.TransmitFile(filePathe);
                    Response.End();
                }
            }
        }

        private void BindStatus()
        {
            List<string> list = new List<string>();

            if (Convert.ToInt32(Session["UserTypeId"]) == 1)
            {
                list.Add("Approve");
            }
            else
            {
                list.Add("Send to Approval");
            }
            list.Add("Reverse");
            list.Add("Reject");

            ddlUpdateStatus.DataSource = list;
            ddlUpdateStatus.DataBind();
            ddlUpdateStatus.Items.Insert(0, new ListItem("-- select status --", ""));
        }

        protected void ddlUpdateStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUpdateStatus.SelectedValue == "Send to Approval")
            {
                sendtoapp.Visible = true;
                reverse.Visible = false;
                reject.Visible = false;
            }
            if (ddlUpdateStatus.SelectedValue == "Reverse")
            {
                sendtoapp.Visible = false;
                reverse.Visible = true;
                reject.Visible = false;
            }
            if (ddlUpdateStatus.SelectedValue == "Reject")
            {
                sendtoapp.Visible = false;
                reverse.Visible = false;
                reject.Visible = true;
            }
        }
    }
}