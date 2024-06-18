namespace Primus.Reports
{
    partial class LogoSchool
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogoSchool));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.sCHOOL_NAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox = new Telerik.Reporting.TextBox();
            this.‰…≈’»’Õ‘«”DataTextBox = new Telerik.Reporting.TextBox();
            this.pHONE_NUMBERSDataTextBox = new Telerik.Reporting.TextBox();
            this.ˆ¡ŒDataTextBox = new Telerik.Reporting.TextBox();
            this.eMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.aDDRESSCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Primus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        SCHOOL_ID, SCHOOL_NAME, ‘¡◊_ƒ…≈’»’Õ”«, ÷¡Œ, EMAIL, ƒ…≈’»’Õ‘«”, √—¡Ã" +
    "Ã¡‘≈…¡ + N\', \' + ‘«À≈÷ŸÕ¡ AS PHONE_NUMBERS\r\nFROM            ”’”_”◊œÀ≈”";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOL_NAMEDataTextBox,
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox,
            this.‰…≈’»’Õ‘«”DataTextBox,
            this.pHONE_NUMBERSDataTextBox,
            this.ˆ¡ŒDataTextBox,
            this.eMAILDataTextBox,
            this.textBox6,
            this.textBox4,
            this.textBox3,
            this.aDDRESSCaptionTextBox,
            this.pictureBox1,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.pictureBox2});
            this.detail.Name = "detail";
            // 
            // sCHOOL_NAMEDataTextBox
            // 
            this.sCHOOL_NAMEDataTextBox.CanGrow = true;
            this.sCHOOL_NAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(4.7396836280822754D));
            this.sCHOOL_NAMEDataTextBox.Name = "sCHOOL_NAMEDataTextBox";
            this.sCHOOL_NAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.sCHOOL_NAMEDataTextBox.Style.Font.Bold = true;
            this.sCHOOL_NAMEDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.sCHOOL_NAMEDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.sCHOOL_NAMEDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.sCHOOL_NAMEDataTextBox.StyleName = "Data";
            this.sCHOOL_NAMEDataTextBox.Value = "=Fields.SCHOOL_NAME";
            // 
            // Ù¡◊_ƒ…≈’»’Õ”«DataTextBox
            // 
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.CanGrow = true;
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2003004550933838D), Telerik.Reporting.Drawing.Unit.Cm(5.59999942779541D));
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.Name = "Ù¡◊_ƒ…≈’»’Õ”«DataTextBox";
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.5996994972229D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.StyleName = "Data";
            this.Ù¡◊_ƒ…≈’»’Õ”«DataTextBox.Value = "=Fields.‘¡◊_ƒ…≈’»’Õ”«";
            // 
            // ‰…≈’»’Õ‘«”DataTextBox
            // 
            this.‰…≈’»’Õ‘«”DataTextBox.CanGrow = true;
            this.‰…≈’»’Õ‘«”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2002003192901611D), Telerik.Reporting.Drawing.Unit.Cm(6.099998950958252D));
            this.‰…≈’»’Õ‘«”DataTextBox.Name = "‰…≈’»’Õ‘«”DataTextBox";
            this.‰…≈’»’Õ‘«”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.599799633026123D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.‰…≈’»’Õ‘«”DataTextBox.StyleName = "Data";
            this.‰…≈’»’Õ‘«”DataTextBox.Value = "=Fields.ƒ…≈’»’Õ‘«”";
            // 
            // pHONE_NUMBERSDataTextBox
            // 
            this.pHONE_NUMBERSDataTextBox.CanGrow = true;
            this.pHONE_NUMBERSDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2002003192901611D), Telerik.Reporting.Drawing.Unit.Cm(6.599998950958252D));
            this.pHONE_NUMBERSDataTextBox.Name = "pHONE_NUMBERSDataTextBox";
            this.pHONE_NUMBERSDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.599799633026123D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.pHONE_NUMBERSDataTextBox.StyleName = "Data";
            this.pHONE_NUMBERSDataTextBox.Value = "=Fields.PHONE_NUMBERS";
            // 
            // ˆ¡ŒDataTextBox
            // 
            this.ˆ¡ŒDataTextBox.CanGrow = true;
            this.ˆ¡ŒDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2003004550933838D), Telerik.Reporting.Drawing.Unit.Cm(7.09999942779541D));
            this.ˆ¡ŒDataTextBox.Name = "ˆ¡ŒDataTextBox";
            this.ˆ¡ŒDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.5996994972229D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.ˆ¡ŒDataTextBox.StyleName = "Data";
            this.ˆ¡ŒDataTextBox.Value = "=Fields.÷¡Œ";
            // 
            // eMAILDataTextBox
            // 
            this.eMAILDataTextBox.CanGrow = true;
            this.eMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2003004550933838D), Telerik.Reporting.Drawing.Unit.Cm(7.59999942779541D));
            this.eMAILDataTextBox.Name = "eMAILDataTextBox";
            this.eMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.5996994972229D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.eMAILDataTextBox.StyleName = "Data";
            this.eMAILDataTextBox.Value = "=Fields.EMAIL";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(3.8979072570800781D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = "ƒ«Ãœ”…¡ ’–«—≈”…¡ ¡–¡”◊œÀ«”«”";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(2.2036402225494385D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = " ¡…  œ…ÕŸÕ… ŸÕ ’–œ»≈”≈ŸÕ";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.7036406993865967D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "’–œ’—√≈…œ ≈—√¡”…¡”";
            // 
            // aDDRESSCaptionTextBox
            // 
            this.aDDRESSCaptionTextBox.CanGrow = true;
            this.aDDRESSCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.203641414642334D));
            this.aDDRESSCaptionTextBox.Name = "aDDRESSCaptionTextBox";
            this.aDDRESSCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.aDDRESSCaptionTextBox.Style.Font.Bold = true;
            this.aDDRESSCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.aDDRESSCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.aDDRESSCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.aDDRESSCaptionTextBox.StyleName = "Caption";
            this.aDDRESSCaptionTextBox.Value = "≈ÀÀ«Õ… « ƒ«Ãœ —¡‘…¡";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3999998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.0093750329688191414D));
            this.pictureBox1.MimeType = "image/gif";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(1.1940666437149048D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(5.59999942779541D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.1999001502990723D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "‘¡◊. ƒ/Õ”«:";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(6.099998950958252D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "–À«—œ÷œ—…≈”:";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(6.599998950958252D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox9.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.StyleName = "Caption";
            this.textBox9.Value = "‘«À≈÷ŸÕ¡:";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(7.09999942779541D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "FAX:";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(7.59999942779541D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "E-MAIL:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.201458215713501D), Telerik.Reporting.Drawing.Unit.Cm(2.7036402225494385D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5985417366027832D), Telerik.Reporting.Drawing.Unit.Cm(1.1940666437149048D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // LogoSchool
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.SCHOOL_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_id.Value"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "LogoSchool";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.ValueMember = "= Fields.SCHOOL_ID";
            reportParameter1.Name = "school_id";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(7.8000001907348633D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox sCHOOL_NAMEDataTextBox;
        private Telerik.Reporting.TextBox Ù¡◊_ƒ…≈’»’Õ”«DataTextBox;
        private Telerik.Reporting.TextBox ‰…≈’»’Õ‘«”DataTextBox;
        private Telerik.Reporting.TextBox pHONE_NUMBERSDataTextBox;
        private Telerik.Reporting.TextBox ˆ¡ŒDataTextBox;
        private Telerik.Reporting.TextBox eMAILDataTextBox;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox aDDRESSCaptionTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.PictureBox pictureBox2;

    }
}