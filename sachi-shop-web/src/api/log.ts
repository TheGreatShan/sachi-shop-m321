import axios from 'axios';

export const getLogs = async () => {
    try {
        
        const apiUrl = import.meta.env.VITE_DB_API_URL + '/api/logs';
        const response = await axios.get(apiUrl);
        return response.data
    } catch (error) {
        console.error('Error fetching logs:', error);
    }
};
