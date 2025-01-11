import { useCart } from "./context/CartContext";

export default function Cart() {
  const { cart, updateCart, clearCart } = useCart();

  console.log(cart.length);

  return (
    <>
      <div className="pt-20">
        <div className="flex flex-col items-center pt-20">
          <div className="text-2xl font-bold mb-5">Your Cart</div>
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
              </div>
            ) : (
              <div className="text-center">No Products</div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
