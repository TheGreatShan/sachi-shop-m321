import { useEffect, useState } from "react";
import Log from "./components/Log";
import { getLogs } from "./api/log";

interface ReceivedLog {
    ID: number;
    CreatedAt: string;
    UpdatedAt: string;
    DeletedAt: null;
    level: string;
    message: string;
    timestamp: string;
    user: string;
}

export default function Dashboard() {
  const [logs, setLogs] = useState<ReceivedLog[]>([]);
  const [countdown, setCountdown] = useState<number>(5);

  const refreshLogs = () => {
    getLogs().then((logs) => {
      setLogs(logs);
      setCountdown(5);
    });
  };

  useEffect(() => {
    refreshLogs();

    const intervalId = setInterval(() => {
      setCountdown((prevCountdown) => {
        if (prevCountdown === 1) {
          refreshLogs();
          return 5;
        } else {
          return prevCountdown - 1;
        }
      });
    }, 1000);

    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className="pt-20">
      <div className="pt-20 flex justify-center items-center flex-col">
        <div className="text-2xl font-bold mb-4">Admin Event Dashboard</div>
        <div className="bg-gray-100 w-1/2 p-5 rounded-lg shadow-lg h-96 overflow-y-auto">
          <div>
            {logs.length > 0 ? (
              logs.map((log, index) => (
                <div key={index} className="mb-2">
                  <Log log={log} />
                </div>
              ))
            ) : (
                <div className="text-center text-gray-500">
                    No events logged
                </div>
            )}
          </div>
        </div>
        <div className="mt-4 text-center text-gray-600">
          Refreshing in {countdown} seconds...
        </div>
      </div>
    </div>
  );
}
