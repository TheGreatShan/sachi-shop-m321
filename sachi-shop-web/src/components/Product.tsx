interface ProductProps {
    id: string;
    product: string;
    description: string;
    stock: number;
}

export default function Product({ id, product, description, stock }: ProductProps) {
    return(
        <div className="bg-gray-100 p-5 rounded-lg shadow-lg m-2 grid grid-cols-8 items-center">
            <div className="col-span-1">
                <img src="/product.svg" alt="product" height={50} width={50} />
            </div>
            <div className="col-span-2">
                <p className="text-lg font-semibold">
                    {product}
                </p>
                <p>
                    Stock: {stock}
                </p>
            </div>
            <div className="col-span-4 text-gray-500">
                <p>
                    {description}
                </p>
            </div>
            <div className="col-span-1 justify-self-end">
                <button className="text-white rounded-lg mr-2">
                    <img src="/plus.svg" alt="add" height={20} width={20}/>
                </button>
            </div>
        </div>
    )
}
