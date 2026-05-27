import { useEffect, useState } from "react"
import Sidebar from "../components/Sidebar";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import LeaveStatus from "../components/ApplyLeave/LeaveStatus";
import ApplyLeaveForm from "../components/ApplyLeave/ApplyLeaveForm";

export default function ApplyLeave(){
    const user = JSON.parse(localStorage.getItem("user"));
    const initials = user?.name?.split(" ").map(n => n[0]).join("").toUpperCase();
    const[active, setActive] = useState("apply");
    const navigate = useNavigate();

    const [leaveBalances, setLeaveBalances] = useState([]);
    const [leaveTypes, setLeaveTypes] = useState([]);

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.href = "/";        
    }
    const employeeId = user?.employeeId;
    useEffect(() => {
        const token = localStorage.getItem("token");
        const getLeaveBalances = async() => {
            const response = await fetch(`http://localhost:5155/api/LeaveBalance/employee/${employeeId}`, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            });
            const result = await response.json(); //an array of leave balances, each with a leaveType, usedDays, and remainingDays
            console.log("Fetched leave balances:", result.data);
            setLeaveBalances(result.data || []);

            const leaveTypeResponse = await fetch(`http://localhost:5155/api/LeaveTypes`, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            });
            const leaveTypeResult = await leaveTypeResponse.json();
            console.log("Fetched leave types:", leaveTypeResult);
            setLeaveTypes(leaveTypeResult|| []);

        }
        getLeaveBalances();

    },[employeeId]); //without this dependency array, the effect would run after every render, which is not what we want. We only want to fetch the leave balances when the component mounts or when the employeeId changes (which is unlikely but good practice to include it as a dependency).
    return(
        
        <div className="app-shell bg-[var(--bg)] min-h-screen p-0 m-0 flex">
            <Sidebar active={active} setActive={setActive} user={user} initials={initials} handleLogout={handleLogout} navigate={navigate}/>
            <div className="flex flex-col w-full p-1">
                <Navbar pageName="Apply Leave"/>
                <div className="main-container flex flex-col gap-4 p-6">
                    <LeaveStatus leaveBalances={leaveBalances}/>
                    <ApplyLeaveForm leaveTypes={leaveTypes} user={user}/>
                </div>
            </div>
        </div>
    )
}
