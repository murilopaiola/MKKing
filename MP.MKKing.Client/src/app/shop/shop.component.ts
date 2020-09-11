import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/product-brand';
import { IType } from '../shared/models/product-type';
import { ShopParams } from '../shared/models/shop-params';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];
  // Child component search input
  @ViewChild('search', { static: true }) searchTerm: ElementRef;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
   }

  getProducts(): void{
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(eventPage: any): void{
    if (this.shopParams.pageNumber !== eventPage){
      this.shopParams.pageNumber = eventPage;
      this.getProducts();
    }
  }

  onSearch(): void {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(): void{
    // Access the element value and set as undefined to reset the search term
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
