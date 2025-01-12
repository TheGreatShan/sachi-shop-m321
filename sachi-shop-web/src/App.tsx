import './App.css'
import testData from "./json/testData.json"

import { useEffect, useState } from 'react'
import { getAllProducts, Product as ProductType, getProductOutOfProductInfo } from './api/stock';
import Product from './components/Product';



function App() {

  const [products, setProducts] = useState<ProductType[]>(testData);  

  useEffect(() => {
    const getProducts = async () => {
      const resp = await getAllProducts()
      setProducts(getProductOutOfProductInfo(resp))
    }

    getProducts()
  }, [])

  return (
    <>
      <div className='pt-20'>
        {
          products.map((item, index) => {
            return (
              <div key={index}>
                <Product
                  id={item.id}
                  product={item.product}
                  description={item.description}
                  stock={item.stock}
                  price={item.price}
                />
              </div>
            )
          })
        }
      </div>
    </>
  )
}

export default App
