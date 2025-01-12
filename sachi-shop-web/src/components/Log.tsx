interface Log {
    ID: number;
    CreatedAt: string;
    UpdatedAt: string;
    DeletedAt: null;
    level: string;
    message: string;
    timestamp: string;
    user: string;
}

export default function Log({ log }: { log: Log }) {
    const levelImage = () => {
        switch(log.level) {
            case "INFO":
                return "/info.svg";
            case "WARNING":
                return "/warning.svg";
            case "CRITICAL":
                return "/critical.svg";
        }
    }

    return(
        <div className="bg-white p-3 rounded-lg shadow-md grid grid-cols-2">
            <div className="col-span-1">
                <div className="text-sm text-gray-500">{log.CreatedAt}</div>
                <div className="text-lg font-bold">{log.level}</div>
                <div className="text-sm">{log.message}</div>
                <div className="text-sm text-gray-500">{log.user}</div>
            </div>
            <div className="col-span-1 flex justify-end items-center">
                <img src={levelImage()} alt="level" height={32} width={32} />
            </div>
        </div>
    )
}