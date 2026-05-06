import { useState } from "react";

export default function Login(){
    return(
        <div className="flex justify-center items-center h-screen bg-gray-100">
            <div className="bg-white p-6 rounded shadow w-[300px]">
                <input 

                    type="text"

                    placeholder="Enter username"

                    className="w-full p-2 border border-red-300 rounded mb-4"

                />
            </div>
        </div>
    )
}