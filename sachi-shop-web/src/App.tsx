import './App.css'
import Navbar from './components/Navbar'
import testData from "./json/testData.json"
import Product from './components/Product'

function App() {
  return (
    <>
      <Navbar />
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
