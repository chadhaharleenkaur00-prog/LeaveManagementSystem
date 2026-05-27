import { useState, useEffect} from "react"
import Sidebar from "../components/Sidebar";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import LeaveRequestsTable from "../components/ApplyLeave/LeaveRequestsTable";

export default function AdminPortal(){
    const user = JSON.parse(localStorage.getItem("user"));
    const initials = user?.name?.split(" ").map(n => n[0]).join("").toUpperCase();
    const[active, setActive] = useState("admin-portal");
    const navigate = useNavigate();
    const[leaveRequests, setLeaveRequests] = useState([]);
    const employeeId = user?.employeeId;

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.href = "/";        
    }
    useEffect(() => {
        const token = localStorage.getItem("token");
        const getLeaveRequests = async() => {
            try {
                const response = await fetch(`http://localhost:5155/api/LeaveRequest/manager/${employeeId}`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                const result = await response.json();
                const leaveRequests = result.data || [];
                setLeaveRequests(leaveRequests);
                console.log("Fetched leave requests for manager:", leaveRequests);
            } catch (error) {
                console.error("Error fetching manager leave requests:", error);
                setLeaveRequests([]);
            }
        }

        if (employeeId) {
            getLeaveRequests();
        }
    }, [employeeId]);

    const handleStatusUpdated = (id, status) => {
        setLeaveRequests((requests) =>
            requests.map((request) =>
                request.id === id ? { ...request, status } : request
            )
        );
    };
    return(
        
        <div className="app-shell bg-[var(--bg)] min-h-screen p-0 m-0 flex">
            <Sidebar active={active} setActive={setActive} user={user} initials={initials} handleLogout={handleLogout} navigate={navigate}/>
            
            <div className="w-full flex flex-col gap-4">
                <Navbar pageName="Admin Portal"/>
                <div className="w-full p-6">
                    <LeaveRequestsTable leaveRequests={leaveRequests} idPresent ={false} employeeName={true} reasonPresent ={true} appliedOnPresent ={true} actionPresent ={true} onStatusUpdated={handleStatusUpdated} />
                </div>
            </div>
        </div>
    )
}
