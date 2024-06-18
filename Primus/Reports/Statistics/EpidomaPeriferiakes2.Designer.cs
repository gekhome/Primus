namespace Primus.Reports.Statistics
{
    partial class EpidomaPeriferiakes2
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpidomaPeriferiakes2));
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sCHOOLYEAR_TEXTGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.sCHOOLYEAR_TEXTGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.sCHOOLYEAR_TEXTDataTextBox = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.sY_TEXTCaptionTextBox = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.shape3 = new Telerik.Reporting.Shape();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sCHOOLYEAR_TEXTGroupFooterSection
            // 
            this.sCHOOLYEAR_TEXTGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.25281482934951782D);
            this.sCHOOLYEAR_TEXTGroupFooterSection.Name = "sCHOOLYEAR_TEXTGroupFooterSection";
            // 
            // sCHOOLYEAR_TEXTGroupHeaderSection
            // 
            this.sCHOOLYEAR_TEXTGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(19.899900436401367D);
            this.sCHOOLYEAR_TEXTGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOLYEAR_TEXTDataTextBox,
            this.subReport1,
            this.sY_TEXTCaptionTextBox,
            this.shape2,
            this.graph1,
            this.textBox1});
            this.sCHOOLYEAR_TEXTGroupHeaderSection.Name = "sCHOOLYEAR_TEXTGroupHeaderSection";
            // 
            // sCHOOLYEAR_TEXTDataTextBox
            // 
            this.sCHOOLYEAR_TEXTDataTextBox.CanGrow = true;
            this.sCHOOLYEAR_TEXTDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.8470833301544189D), Telerik.Reporting.Drawing.Unit.Cm(6.5999999046325684D));
            this.sCHOOLYEAR_TEXTDataTextBox.Name = "sCHOOLYEAR_TEXTDataTextBox";
            this.sCHOOLYEAR_TEXTDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.9941678047180176D), Telerik.Reporting.Drawing.Unit.Cm(0.69999885559082031D));
            this.sCHOOLYEAR_TEXTDataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.sCHOOLYEAR_TEXTDataTextBox.Style.Font.Bold = true;
            this.sCHOOLYEAR_TEXTDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.sCHOOLYEAR_TEXTDataTextBox.StyleName = "Data";
            this.sCHOOLYEAR_TEXTDataTextBox.Value = "=Fields.SCHOOLYEAR_TEXT";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Primus.Reports.LogoA2, Primus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=n" +
    "ull";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.84708309173584D), Telerik.Reporting.Drawing.Unit.Cm(5.4000000953674316D));
            // 
            // sY_TEXTCaptionTextBox
            // 
            this.sY_TEXTCaptionTextBox.CanGrow = true;
            this.sY_TEXTCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(6.5999999046325684D));
            this.sY_TEXTCaptionTextBox.Name = "sY_TEXTCaptionTextBox";
            this.sY_TEXTCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.8468830585479736D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.sY_TEXTCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.sY_TEXTCaptionTextBox.Style.Font.Bold = true;
            this.sY_TEXTCaptionTextBox.Style.Font.Name = "Tahoma";
            this.sY_TEXTCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.sY_TEXTCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.sY_TEXTCaptionTextBox.StyleName = "Caption";
            this.sY_TEXTCaptionTextBox.Value = "свокийо етос:";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(7.3001995086669922D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.814792633056641D), Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D));
            // 
            // graph1
            // 
            graphGroup1.Label = "пеяижеяеиайес";
            graphGroup1.Name = "graphGroup";
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource1;
            this.graph1.Filters.Add(new Telerik.Reporting.Filter("=Fields.свокийо_етос", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolyear.Value"));
            this.graph1.Legend.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            this.graph1.Legend.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.graph1.Legend.Style.Font.Strikeout = false;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Legend.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.graph1.Legend.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0885419100522995D), Telerik.Reporting.Drawing.Unit.Cm(7.8000001907348633D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.752712249755859D), Telerik.Reporting.Drawing.Unit.Cm(12.09990119934082D));
            this.graph1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.graph1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            // 
            // cartesianCoordinateSystem1
            // 
            this.cartesianCoordinateSystem1.Name = "cartesianCoordinateSystem1";
            this.cartesianCoordinateSystem1.XAxis = this.graphAxis1;
            this.cartesianCoordinateSystem1.YAxis = this.graphAxis2;
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis1";
            this.graphAxis1.Scale = categoryScale1;
            // 
            // graphAxis2
            // 
            this.graphAxis2.LabelFormat = "{0:P0}";
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.MinorTickMarkDisplayType = Telerik.Reporting.GraphAxisTickMarkDisplayType.Inside;
            this.graphAxis2.Name = "GraphAxis2";
            numericalScale1.MajorStep = 0.1D;
            numericalScale1.Minimum = 0D;
            numericalScale1.MinorStep = 0.05D;
            this.graphAxis2.Scale = numericalScale1;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Primus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = resources.GetString("sqlDataSource1.SelectCommand");
            // 
            // barSeries1
            // 
            this.barSeries1.CategoryGroup = graphGroup1;
            this.barSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.barSeries1.DataPointLabel = "= Fields.дапамг_пососто";
            this.barSeries1.DataPointLabelFormat = "{0:P1}";
            this.barSeries1.DataPointStyle.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.barSeries1.LegendItem.MarkStyle.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.barSeries1.LegendItem.MarkStyle.LineStyle = Telerik.Reporting.Drawing.LineStyle.Solid;
            this.barSeries1.LegendItem.Style.BackgroundColor = System.Drawing.Color.Transparent;
            this.barSeries1.LegendItem.Style.Font.Name = "Tahoma";
            this.barSeries1.LegendItem.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.barSeries1.LegendItem.Style.LineColor = System.Drawing.Color.Transparent;
            this.barSeries1.LegendItem.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries1.LegendItem.Value = "= Fields.PERIFERIAKI_TEXT";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.PERIFERIAKI_TEXT"));
            graphGroup2.Name = "seriesGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.пкгхос_пососто", Telerik.Reporting.SortDirection.Desc));
            this.barSeries1.SeriesGroup = graphGroup2;
            this.barSeries1.Y = "= Fields.дапамг_пососто";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0885419100522995D), Telerik.Reporting.Drawing.Unit.Cm(5.4001998901367188D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.814792633056641D), Telerik.Reporting.Drawing.Unit.Cm(0.70000046491622925D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "йатамолг дапамгс стецасгс & ситисгс ама пеяижеяеиайг диеухумсг";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000105500221252D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            this.labelsGroupHeaderSection.Style.Visible = true;
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Primus.Properties.Settings.DBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        SCHOOLYEAR_ID, SCHOOLYEAR_TEXT\r\nFROM            сус_свокийа_етг\r\nOR" +
    "DER BY SCHOOLYEAR_TEXT";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.99937492609024048D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape3,
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D), Telerik.Reporting.Drawing.Unit.Cm(0.14062449336051941D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.682291030883789D), Telerik.Reporting.Drawing.Unit.Cm(0.20000070333480835D));
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.35229113698005676D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2054166793823242D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.841041088104248D), Telerik.Reporting.Drawing.Unit.Cm(0.35229113698005676D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"сЕК. \" + PageNumber + \" АПЭ \" + PageCount";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox3,
            this.textBox5,
            this.textBox4});
            this.detail.Name = "detail";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Format = "{0:C2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.83311653137207D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0081396102905273D), Telerik.Reporting.Drawing.Unit.Cm(0.49989962577819824D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Calibri";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "= Fields.дапамг_етос";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9001998901367188D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9997997283935547D), Telerik.Reporting.Drawing.Unit.Cm(0.49989962577819824D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Calibri";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "= Fields.PERIFERIAKI_TEXT";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.900199890136719D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9327163696289063D), Telerik.Reporting.Drawing.Unit.Cm(0.49989962577819824D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Calibri";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "дапамг:";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0885419100522995D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8114581108093262D), Telerik.Reporting.Drawing.Unit.Cm(0.49989962577819824D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Calibri";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "пеяижеяеиайг :";
            // 
            // EpidomaPeriferiakes2
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.свокийо_етос", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolyear.Value"));
            group1.GroupFooter = this.sCHOOLYEAR_TEXTGroupFooterSection;
            group1.GroupHeader = this.sCHOOLYEAR_TEXTGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.SCHOOLYEAR_TEXT"));
            group1.Name = "sCHOOLYEAR_TEXTGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOLYEAR_TEXTGroupHeaderSection,
            this.sCHOOLYEAR_TEXTGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.detail});
            this.Name = "EpidomaPeriferiakes1";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlSchoolYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.SCHOOLYEAR_TEXT";
            reportParameter1.AvailableValues.ValueMember = "= Fields.SCHOOLYEAR_ID";
            reportParameter1.Name = "schoolyear";
            reportParameter1.Text = "сВОКИЙЭ щТОР";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.999998092651367D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection sCHOOLYEAR_TEXTGroupHeaderSection;
        private Telerik.Reporting.TextBox sCHOOLYEAR_TEXTDataTextBox;
        private Telerik.Reporting.GroupFooterSection sCHOOLYEAR_TEXTGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox sY_TEXTCaptionTextBox;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Shape shape3;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.TextBox textBox1;

    }
}