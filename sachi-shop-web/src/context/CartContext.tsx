import { createContext, useState, useContext, ReactNode } from "react";

interface CartProduct {
  id: string;
  product: string;
  description: string;
  stock: number;
  price: number;
  count: number;
}

interface Product {
    id: string;
    product: string;
    description: string;
    stock: number;
    price: number;
  }

interface CartContextType {
  cart: CartProduct[];
  addToCart: (product: CartProduct) => void;
  removeFromCart: (id: string) => void;
  updateCart: (id: string, count: number) => void;
  clearCart: () => void;
}

interface CartProviderProps {
  children: ReactNode;
}

const defaultCartContext: CartContextType = {
  cart: [],
  addToCart: () => {},
  removeFromCart: () => {},
  updateCart: () => {},
  clearCart: () => {},
};

const CartContext = createContext<CartContextType>(defaultCartContext);

export const CartProvider = ({ children }: CartProviderProps) => {
  const [cart, setCart] = useState<CartProduct[]>([]);

  const addToCart = (product: Product) => {
    setCart((prevCart) => {
      const productExists = prevCart.find((item) => item.id === product.id);

      if (productExists) {
        return prevCart.map((item) =>
          item.id === product.id
            ? { ...item, count: item.count + 1 }
            : item
        );
      } else {
        return [...prevCart, { ...product, count: 1 }];
      }
    });
  };

  const removeFromCart = (id: string) => {
    setCart((prevCart) => prevCart.filter((item) => item.id !== id));
  };

  const updateCart = (id: string, count: number) => {
    setCart((prevCart) => {
      if (count === 0) {
        return prevCart.filter((item) => item.id !== id);
      }
      return prevCart.map((item) =>
        item.id === id ? { ...item, count: Math.max(count, 0) } : item
      );
    });
  };
  

  const clearCart = () => {
    setCart([]);
  };

  return (
    <CartContext.Provider value={{ cart, addToCart, removeFromCart, updateCart, clearCart }}>
      {children}
    </CartContext.Provider>
  );
};

// Custom hook to access the CartContext
export const useCart = (): CartContextType => {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error("useCart must be used within a CartProvider");
  }
  return context;
};
