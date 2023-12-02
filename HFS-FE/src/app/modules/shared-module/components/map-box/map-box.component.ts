import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import * as mapboxgl from 'mapbox-gl';
import { LngLatLikeModel } from '../../models/map-box.model';



@Component({
  selector: 'app-map-box',
  templateUrl: './map-box.component.html',
  styleUrls: ['./map-box.component.scss']
})
export class MapBoxComponent implements OnInit, AfterViewInit{
  map: mapboxgl.Map;

  @Input() position: LngLatLikeModel;
  @Input() geometries: GeoJSON.GeometryObject[];

  constructor(){
    this.position = new LngLatLikeModel(105.8544441, 21.0294498)
    

  }
  ngAfterViewInit(): void {
    
  }

  ngOnInit() {
    // this.map = new mapboxgl.Map({
    //   container: "map",
    //   style: "mapbox://styles/bluerhino/cjd2cc3t53a1y2sp3cuz1w6hk",
    //   center: [105.85607, 15.868349],
    //   zoom: 2.5
    // })

  }

  
}
