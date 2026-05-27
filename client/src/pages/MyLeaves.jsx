import { useEffect, useState } from "react"
import Sidebar from "../components/Sidebar";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import LeaveRequestsTable from "../components/ApplyLeave/LeaveRequestsTable";

export default function MyLeaves(){
    const user = JSON.parse(localStorage.getItem("user"));
    const initials = user?.name?.split(" ").map(n => n[0]).join("").toUpperCase();
    const[active, setActive] = useState("my-leaves");
    const navigate = useNavigate();


    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.href = "/";        
    }
    const [leaveRequests, setLeaveRequests] = useState([]);
    const employeeId = user?.employeeId;
    
    useEffect(() => {
        const getLeaveRequests = async() => {
            try {
                const token = localStorage.getItem("token");
                const response = await fetch(`http://localhost:5155/api/LeaveRequest/employee/${employeeId}`, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });
                const result = await response.json();
                const requests = result.data || [];
                console.log("Fetched leave requests:", result);
                console.log("Fetched leave requests:", requests);
                setLeaveRequests(requests);
            } catch (error) {
                console.error("Error fetching leave requests:", error);
                setLeaveRequests([]);
            }
        }

        if (employeeId) {
            getLeaveRequests();
        }
    }, [employeeId]); //Run this effect whenever employeeId changes.
    const reasonPresent = true;
    const appliedOnPresent = true;
    return(
        
        <div className="app-shell bg-[var(--bg)] min-h-screen p-0 m-0 flex">
            <Sidebar active={active} setActive={setActive} user={user} initials={initials} handleLogout={handleLogout} navigate={navigate}/>
            <div className="flex flex-col min-h-screen gap-1 w-full">
            <Navbar pageName="My Leaves"/>
            <div className="leave-table p-6 pt-0">
                <LeaveRequestsTable leaveRequests={leaveRequests} reasonPresent ={reasonPresent} appliedOnPresent ={appliedOnPresent} />
            </div>
            </div>
        </div>
    )
}
