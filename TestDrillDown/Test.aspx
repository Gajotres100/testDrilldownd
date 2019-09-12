<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TestDrillDown.Test1" MasterPageFile="~/Test.Master" CodeFile="Test.aspx.cs" %>

<%@ Register assembly="DevExpress.XtraCharts.v18.2.Web, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <script type="text/javascript">
<!--
    function SetSummaryKindVisible(isVisible) {
        optionsLayout.rootItem.items[0].SetVisible(isVisible);
    }
    //-->of us
    </script>
    <dx:WebChartControl ID="WebChartControl1" runat="server" Height="400px" Width="700px"        
        ClientInstanceName="chart"
        OnBoundDataChanged="WebChartControl1_BoundDataChanged"
        OnDrillDownStateChanged="WebChartControl1_DrillDownStateChanged"
        SeriesDataMember="ProductCategory"
        ToolTipEnabled="False" CrosshairEnabled="True">
        <BorderOptions Visibility="False" />
        <CrosshairOptions CrosshairLabelMode="ShowForNearestSeries"/>
        <Titles>
            <dx:ChartTitle Text="Title"/>
        </Titles>
        <SeriesTemplate ArgumentDataMember="Region" ValueDataMembersSerializable="Sales" LabelsVisibility="False"
            CrosshairLabelPattern="{S}: {V:N2} Kn">            
            <ViewSerializable>
                <dx:StackedBarSeriesView/>
            </ViewSerializable>
            <QualitativeSummaryOptions SummaryFunction="SUM([Sales])"/>
            <SeriesPointDrillTemplate SeriesDataMember="ProductName" ArgumentDataMember="SaleDate" ValueDataMembersSerializable="Sales"
                CrosshairLabelPattern="{S}: {V:N2} Kn">
                <ViewSerializable>
                    <dx:StackedAreaSeriesView Transparency="100"/>
                </ViewSerializable>
                <SeriesDrillTemplate SeriesDataMember="ProductName" ArgumentDataMember="SaleDate" ValueDataMembersSerializable="Sales"
                    CrosshairLabelPattern="{A:d}: {V:N2} Kn">
                    <ViewSerializable>
                    <dx:SplineAreaSeriesView Transparency="100" MarkerVisibility="false"/>
                    </ViewSerializable>
                </SeriesDrillTemplate>
            </SeriesPointDrillTemplate>
            <ArgumentDrillTemplate SeriesDataMember="ProductCategory" ArgumentDataMember="SaleDate" ValueDataMembersSerializable="Sales"
                CrosshairLabelPattern="{S}: {V:N2} Kn">
                    <ViewSerializable>
                        <dx:StackedAreaSeriesView Transparency="100"/>
                    </ViewSerializable>
                <SeriesDrillTemplate SeriesDataMember="ProductName" ArgumentDataMember="SaleDate" ValueDataMembersSerializable="Sales"
                    CrosshairLabelPattern="{S}: {V:N2} $">
                    <ViewSerializable>
                        <dx:StackedAreaSeriesView Transparency="100"/>
                    </ViewSerializable>
                    <SeriesDrillTemplate SeriesDataMember="ProductName" ArgumentDataMember="SaleDate" ValueDataMembersSerializable="Sales"
                        CrosshairLabelPattern="{A:d}: {V:N2} $">
                        <ViewSerializable>
                            <dx:SplineAreaSeriesView Transparency="100"  MarkerVisibility="false"/>
                        </ViewSerializable>
                </SeriesDrillTemplate>
                </SeriesDrillTemplate>
            </ArgumentDrillTemplate>
        </SeriesTemplate>
        <DiagramSerializable>
            <dx:XYDiagram Rotated="true">
                <AxisX >
                    <GridLines Visible="True"></GridLines>
                    <Label></Label>  
                </AxisX>
                <AxisY Title-Text="Tisuća Kuna" Title-Visibility="True" VisibleInPanesSerializable="-1">
                    <Label TextPattern = "{V:0,.##}"></Label>
                </AxisY>
            </dx:XYDiagram>
        </DiagramSerializable>
    </dx:WebChartControl>
</asp:Content>