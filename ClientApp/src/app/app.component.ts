import { TemperatureReadingClient, TemperatureReading } from 'src/app/Core/Services/api.service';

import { Component, NgZone, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import * as am4core from '@amcharts/amcharts4/core';
import * as am4charts from '@amcharts/amcharts4/charts';
import am4themes_animated from '@amcharts/amcharts4/themes/animated';

am4core.useTheme(am4themes_animated);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  public temperatureReadings: TemperatureReading[];

  private chart: am4charts.XYChart;

  constructor(private temperatureClient: TemperatureReadingClient, private zone: NgZone) {

  }
  title = 'ClientApp';

  ngOnInit() {

  }
  ngAfterViewInit() {
    this.zone.runOutsideAngular(() => {
      let chart = am4core.create('chartdiv', am4charts.XYChart);

      // ... chart code goes here ...
      let data = [];
      this.temperatureClient.getAll().subscribe(x => {
        this.temperatureReadings = x;
        this.temperatureReadings.forEach(y => {
          data.push({tijd:  new Date(y.timeStamp), temperatuur: y.value});
        });
        console.log(data);

        chart.data = data;
  
        let dateAxis = chart.xAxes.push(new am4charts.DateAxis());
        dateAxis.renderer.grid.template.location = 0;
  
        let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.tooltip.disabled = true;
        valueAxis.renderer.minWidth = 35;
  
        dateAxis.title.text = 'Tijd';
        valueAxis.title.text = 'Temperatuur (°C)';
  
        let lineSeries = chart.series.push(new am4charts.LineSeries());
        lineSeries.dataFields.dateX = 'tijd';
        lineSeries.dataFields.valueY = 'temperatuur';
  
        lineSeries.tooltipText = '{valueY.value}';
        chart.cursor = new am4charts.XYCursor();
  
        let scrollbarX = new am4charts.XYChartScrollbar();
        scrollbarX.series.push(lineSeries);
        chart.scrollbarX = scrollbarX;
  
        this.chart = chart;
      });
     
    });
  }

  ngOnDestroy() {
    this.zone.runOutsideAngular(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
  }
}
