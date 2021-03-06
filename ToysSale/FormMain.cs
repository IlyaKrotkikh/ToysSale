﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;

namespace ToysSale
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
            this.tsmMagasineManage.Enabled =
                tsmSaleManage.Enabled = false;
            tsmSetUpConnection_Click(this, null);
            this.Text = "Магазин игрушек (отключено)";
        }

        private void SetUpDatabase(object o)
        {
            try
            {
                ToysSale.dbToySale = o as IMongoDatabase;
                ToysSale.ConnState = true;
                this.tsmMagasineManage.Enabled =
                    tsmSaleManage.Enabled = tsmReport.Enabled = tsmNewSale.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка.");
                ToysSale.dbToySale = null;
                ToysSale.ConnState = this.tsmMagasineManage.Enabled =
                    tsmSaleManage.Enabled = tsmReport.Enabled = tsmNewSale.Enabled = false;
                this.Text = "Магазин игрушек (отключено)";
            }
            finally
            {
                if (ToysSale.dbToySale == null)
                {
                    this.Text = "Магазин игрушек (отключено)";
                    ToysSale.ConnState = this.tsmMagasineManage.Enabled =
                    tsmSaleManage.Enabled = tsmReport.Enabled = tsmNewSale.Enabled = false;
                }
                else
                    this.Text = "Подключен к " + ToysSale.dbToySale.DatabaseNamespace.ToString();
            }
        }

        private void tsmSetUpConnection_Click(object sender, EventArgs e)
        {
            FormConnect frm = new FormConnect(SetUpDatabase);
            frm.MdiParent = this;
            frm.Show();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmManageNomenclature_Click(object sender, EventArgs e)
        {
            ControlNomenclature control = new ControlNomenclature(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Номенклатура");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmManageStaff_Click(object sender, EventArgs e)
        {
            ControlStaff control = new ControlStaff(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Персонал");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmManageDepartment_Click(object sender, EventArgs e)
        {
            ControlDepartment control = new ControlDepartment(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Подразделение компании");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmManageSupply_Click(object sender, EventArgs e)
        {
            ControlOrderedGoods control = new ControlOrderedGoods(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Поставки");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmManageClients_Click(object sender, EventArgs e)
        {
            ControlClient control = new ControlClient(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Клиенты");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmManageGoods_Click(object sender, EventArgs e)
        {
            ControlExistingGoods control = new ControlExistingGoods(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Товар в магазине");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmDebitGoods_Click(object sender, EventArgs e)
        {
            ControlExistingGoods control = new ControlExistingGoods(ToysSale.dbToySale);
            try
            {
                MessageBox.Show(control.FormDebetOrderedGoods(), "Выполнено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка.");
            }
        }

        private void tsmSaleManage_Click(object sender, EventArgs e)
        {
            ControlSale control = new ControlSale(ToysSale.dbToySale);
            ControlStaff controlstaff = new ControlStaff(ToysSale.dbToySale);
            FormChoice frm = new FormChoice(controlstaff.GetList(), control.SetStaff, "Выберите работника!");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                FormListItems form = new FormListItems(control, "Продажи");
                form.MdiParent = this;
                form.Show();
            }
        }

        private void tsmManagediscount_Click(object sender, EventArgs e)
        {
            ControlDiscount control = new ControlDiscount(ToysSale.dbToySale);
            FormListItems form = new FormListItems(control, "Скидки");
            form.MdiParent = this;
            form.Show();
        }

        private void tsmNewSale_Click(object sender, EventArgs e)
        {
            ControlSale control = new ControlSale(ToysSale.dbToySale);
            ControlStaff controlstaff = new ControlStaff(ToysSale.dbToySale);
            FormChoice frm = new FormChoice(controlstaff.GetList(), control.SetStaff, "Выберите работника!");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                try
                {
                    control.Add();
                }
                catch
                {
                    MessageBox.Show("Запись не создана!", "Ошибка.");
                }
            }
        }

        private void tsmQDebitGoods_Click(object sender, EventArgs e)
        {
            ControlExistingGoods control = new ControlExistingGoods(ToysSale.dbToySale);
            try
            {
                MessageBox.Show(control.QDebetOrderedGoods(), "Выполнено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка.");
            }
        }

        private void tsmReportOfDeliveries_Click(object sender, EventArgs e)
        {
            ControlOrderedGoods control = new ControlOrderedGoods(ToysSale.dbToySale);
            FormReportDelivery form = new FormReportDelivery(control);
            form.MdiParent = this;
            form.Show();
        }

        private void tsmReportOfSales_Click(object sender, EventArgs e)
        {
            ControlSale control = new ControlSale(ToysSale.dbToySale);
            FormReportSale form = new FormReportSale(control);
            form.MdiParent = this;
            form.Show();
        }
    }
}