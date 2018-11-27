namespace ConvenientStore_Pro
{
    partial class frm_ReprintBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.billCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_App = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBill
            // 
            this.dgvBill.AllowUserToDeleteRows = false;
            this.dgvBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.billCode,
            this.day,
            this.total});
            this.dgvBill.Location = new System.Drawing.Point(0, 0);
            this.dgvBill.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.ReadOnly = true;
            this.dgvBill.Size = new System.Drawing.Size(494, 319);
            this.dgvBill.TabIndex = 0;
            // 
            // billCode
            // 
            this.billCode.DataPropertyName = "billcode";
            this.billCode.HeaderText = "Bill Code";
            this.billCode.Name = "billCode";
            this.billCode.ReadOnly = true;
            // 
            // day
            // 
            this.day.DataPropertyName = "day";
            this.day.HeaderText = "Day of Sale";
            this.day.Name = "day";
            this.day.ReadOnly = true;
            this.day.Width = 200;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            this.total.HeaderText = "Total Money";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Width = 150;
            // 
            // btn_App
            // 
            this.btn_App.Location = new System.Drawing.Point(355, 326);
            this.btn_App.Name = "btn_App";
            this.btn_App.Size = new System.Drawing.Size(112, 30);
            this.btn_App.TabIndex = 1;
            this.btn_App.Text = "Apply";
            this.btn_App.UseVisualStyleBackColor = true;
            this.btn_App.Click += new System.EventHandler(this.btn_App_Click);
            // 
            // frm_ReprintBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 362);
            this.Controls.Add(this.btn_App);
            this.Controls.Add(this.dgvBill);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frm_ReprintBill";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reprint";
            this.Load += new System.EventHandler(this.frm_ReprintBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn billCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn day;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Button btn_App;
    }
}