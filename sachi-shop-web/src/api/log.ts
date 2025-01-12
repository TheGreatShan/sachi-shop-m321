import axios from 'axios';

interface Log {
    level: string;
    message: string;
    timestamp: Date;
    user: string;
}

export const getLogs = async () => {
    try {
        const apiUrl = import.meta.env.VITE_DB_API_URL + '/api/logs';
        const response = await axios.get(apiUrl);
        return response.data
    } catch (error) {
        console.error('Error fetching logs:', error);
    }
};

export const produceLog = async (log: Log) => {
    try {
        const apiUrl = import.meta.env.VITE_KAFKA_API_URL + '/produce?topic=logs'
        console.log(apiUrl)
        const response = await axios.post(apiUrl, log)
        return response.data
    } catch (error) {
        console.error('Error posting logs', error)
    }
}
