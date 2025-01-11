import { useCart } from "../context/CartContext";

interface CartProduct {
    id: string;
    product: string;
    description: string;
    stock: number;
    price: number;
    count: number;
  }

interface CartProductProps {
  id: string;
  product: string;
  description: string;
  stock: number;
  price: number;
}

export default function CartProduct({
  id,
  product,
  description,
  stock,
  price,
}: CartProductProps) {
    const { addToCart } = useCart();

  return (
    <div className="bg-gray-100 p-5 rounded-lg shadow-lg m-2 grid grid-cols-8 items-center">
      <div className="col-span-1">
        <img src="/product.svg" alt="product" height={50} width={50} />
      </div>
      <div className="col-span-2">
        <p className="text-lg font-semibold text-red-500">{price}.-</p>
        <p className="text-lg font-semibold">{product}</p>
        <p>
          {stock > 0 ? (
            <span>{stock} in stock</span>
          ) : (
            <span className="text-red-500">Out of Stock</span>
          )}
        </p>
      </div>
      <div className="col-span-4 text-gray-500">
        <p>{description}</p>
      </div>
      <div className="col-span-1 justify-self-end">
        <button
          className="text-white rounded-lg mr-2"
          onClick={() =>
            addToCart({ id, product, description, stock, price })
          }
        >
          <img src="/plus.svg" alt="add" height={20} width={20} />
        </button>
      </div>
    </div>
  );
}
