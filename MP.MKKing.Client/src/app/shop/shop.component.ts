import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/product-brand';
import { IType } from '../shared/models/product-type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = 'name';
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
   }

  getProducts(): void{
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {
      this.products = response.data;
    }, err => {
      console.log(err);
    });
  }

  getBrands(): void{
    this.shopService.getBrands().subscribe(response => {
      /// ... = spread operator
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, err => {
      console.log(err);
    });
  }

  getTypes(): void{
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, err => {
      console.log(err);
    });
  }

  onBrandSelected(brandId: number): void {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    this.sortSelected = sort;
    this.getProducts();
  }

}
