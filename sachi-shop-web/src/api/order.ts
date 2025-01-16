import axios from "axios";
import { ApiCartProduct } from "../Cart";

export async function orderProducts(order: ApiCartProduct)
{
    try {
        const apiUrl = import.meta.env.VITE_EUREKA_API_URL + '/order-service/order'
        console.log(apiUrl)
        const response = await axios.post(apiUrl, order)
        return response.data
    } catch (error) {
        console.error('Error posting order', error)
    }
}