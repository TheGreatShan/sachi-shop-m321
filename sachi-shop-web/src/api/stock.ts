import axios from "axios";

export interface Product {
    id: string;
    product: string;
    description: string;
    stock: number;
    price: number;
}

export interface Information {
    id : string;
    productId : string;
    information : string;
    stage : string
}

export interface ProductInfo {
    product : Product;
    informations : Information[]
}

export async function getAllProducts() : Promise<ProductInfo[]> {
    const apiUrl = import.meta.env.VITE_GATEWAY_URL + '/stock-service/products';
    const {data} = await axios.get<ProductInfo[]>(apiUrl);
    
    return data
}

export function getProductOutOfProductInfo(productInfo : ProductInfo[]) : Product[]{
    const productList : Product[] = []
    productInfo.forEach(x => productList.push(x.product))

    return productList;
}