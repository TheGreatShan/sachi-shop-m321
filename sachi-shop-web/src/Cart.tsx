import { useCart } from "./context/CartContext";

import { produceLog } from "./api/log";

interface Log {
  level: string;
  message: string;
  timestamp: Date;
  user: string;
}

interface CartProduct {
  id: string;
  product: string;
  description: string;
  stock: number;
  price: number;
  count: number;
}

export default function Cart() {
  const { cart, updateCart, clearCart } = useCart();

  const totalItems = cart.reduce((acc, item) => acc + item.count, 0);

  const totalCost = cart.reduce((acc, item) => acc + item.price * item.count, 0);

  const handleOrder = async() => {
    const now = new Date()

    const log : Log = {
      "level": "INFO",
      "message": "John ordered " + totalItems + " items, costing " + totalCost + ".-",
      "timestamp": now,
      "user": ""
    }

    await produceLog(log)
  }

  return (
    <>
      <div className="pt-20">
        <div className="flex flex-col items-center pt-20">
          <div className="text-4xl font-bold mb-5">Your Cart</div>
          <div className="bg-gray-100 w-1/2 p-5 rounded-lg shadow-lg">
            {cart.length > 0 ? (
              <div>
                {cart.map((item, index) => {
                  return (
                    <>
                      <div
                        key={index}
                        className="bg-gray-200 p-5 rounded-lg shadow-lg m-2 grid grid-cols-8 items-center"
                      >
                        <div className="col-span-1">
                          <img
                            src="/product.svg"
                            alt="product"
                            height={50}
                            width={50}
                          />
                        </div>
                        <div className="col-span-2">
                          <p className="text-lg font-semibold text-red-500">
                            {item.price}.-
                          </p>
                          <p className="text-lg font-semibold">
                            {item.product}
                          </p>
                          <p>
                            {item.stock > 0 ? (
                              <span>{item.stock} in stock</span>
                            ) : (
                              <span className="text-red-500">Out of Stock</span>
                            )}
                          </p>
                        </div>
                        <div className="col-span-4 text-gray-500">
                          <p>{item.description}</p>
                        </div>
                        <div className="col-span-1 grid grid-cols-3 items-center justify-items-center">
                          <button className="text-white rounded-lg mr-2" onClick={() => updateCart(item.id, item.count - 1)}>
                            <img
                              src="/minus.svg"
                              alt="remove"
                              height={15}
                              width={15}
                            />
                          </button>
                          <div className="text-center">{item.count}</div>
                          <button className="text-white rounded-lg mr-2" onClick={() => updateCart(item.id, item.count + 1)}>
                            <img
                              src="/plus.svg"
                              alt="add"
                              height={15}
                              width={15}
                            />
                          </button>
                        </div>
                      </div>
                    </>
                  );
                })}
                <button className="bg-gray-150 rounded-lg px-4 py-2 hover:bg-gray-200" onClick={clearCart}>
                    Clear Cart
                </button>
                <div className="grid grid-cols-2 mt-5">
                  <div className="text-xl font-bold">Total Items: {totalItems}</div>
                  <div className="text-xl text-red-500 font-bold justify-self-end">{totalCost.toFixed(1)}.-</div>
                </div>
              </div>
            ) : (
              <div className="text-center">No Products</div>
            )}
          </div>
        </div>
      </div>
      <div className="flex justify-center pt-5">
            <button
              className="bg-gray-200 px-3 py-2 rounded-lg hover:bg-gray-300"
              onClick={() => handleOrder()}
            >
              Order
            </button>
      </div>
    </>
  );
}
