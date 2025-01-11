import './App.css'
import testData from "./json/testData.json"
import Product from './components/Product'

import { useState } from 'react'

interface Product {
  id: string;
  product: string;
  description: string;
  stock: number;
  price: number;
}

function App() {

  const [products, setProducts] = useState<Product[]>(testData);  

  return (
    <>
      <div className='pt-20'>
        {
          testData.map((item, index) => {
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
