import { Link } from "react-router-dom"

export default function Profile() {
    return(
        <>
            <div className='pt-20'>
                <div className="pt-20 w-full flex justify-center">
                    <div className="bg-gray-100 w-1/2 p-5 rounded-lg shadow-lg">
                        <div className="grid grid-cols-5 gap-5">
                            <div className="col-span-1">
                                <img src="/person.svg" alt="person" height={200} width={200} />
                            </div>
                            <div className="col-span-1">
                                <p className="font-bold text-2xl">
                                    Profile
                                </p>
                                <div className="grid grid-cols-2 gap-1">
                                    <div>
                                        <p>
                                            First Name:
                                        </p>
                                        <p>
                                            Last Name:
                                        </p>
                                        <p>
                                            Role:
                                        </p>
                                    </div>
                                    <div>
                                        <p>
                                            John
                                        </p>
                                        <p>
                                            Doe
                                        </p>
                                        <p className="text-red-500">
                                            Admin
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="mt-5 text-center">
                            <Link to={"/dashboard"}>
                                <button className="bg-blue-500 text-white px-5 py-2 rounded-lg hover:bg-blue-600">
                                    Admin Dashboard
                                </button>
                            </Link>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}