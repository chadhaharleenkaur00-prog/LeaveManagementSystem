import { useState, useEffect } from "react"
import Sidebar from "../components/Sidebar";
import { useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import { StatsCard } from "../components/StatsCard";
import LeaveBalanceCard from "../components/LeaveBalanceCard";
import LeaveRequestsTable from "../components/ApplyLeave/LeaveRequestsTable";

export default function Dashboard(){
    const user = JSON.parse(localStorage.getItem("user"));
    const initials = user?.name?.split(" ").map(n => n[0]).join("").toUpperCase();
    const employeeId = user?.employeeId;
    const[active, setActive] = useState("dashboard");
    const navigate = useNavigate();
    const [balances, setBalances] = useState([]);
    const [totalLeaves, setTotalLeaves] = useState(0);
    const [leaveRequests, setLeaveRequests] = useState([]);
    const [approvedCount, setApprovedCount] = useState(0);
    const [pendingCount, setPendingCount] = useState(0);
    const idPresent = false;
    const reasonPresent = false;
    const appliedOnPresent = false;  
    const actionPresent = false;
    useEffect(() => {
        const token = localStorage.getItem("token");
        const getTotalLeaves = async() => {
            const response = await fetch(`http://localhost:5155/api/LeaveBalance/employee/${employeeId}`, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            });
            const result = await response.json();
            const balances = result.data || [];
            setBalances(balances);
            const total = balances.reduce((sum, balance) => sum + balance.remainingDays, 0);
            setTotalLeaves(total);
            console.log("Fetched leave balances:", balances);
            console.log("user:", user);
        }

        const getLeaveRequests = async() => {
            try {
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

                const approved = requests.filter(request => request.status?.toLowerCase() === "approved").length;
                const pending = requests.filter(request => request.status?.toLowerCase() === "pending").length;
                setApprovedCount(approved);
                setPendingCount(pending);
            } catch (error) {
                console.error("Error fetching leave requests:", error);
                setLeaveRequests([]);
                setApprovedCount(0);
                setPendingCount(0);
            }
        }

        if (employeeId) {
            getTotalLeaves();
            getLeaveRequests();
        }
    }, [employeeId]); //Run this effect whenever employeeId changes.

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.href = "/";        
    }

    return(
        <div className="app-shell bg-[var(--bg)] min-h-screen p-0 m-0 flex">
            <Sidebar active={active} setActive={setActive} user={user} initials={initials} handleLogout={handleLogout} navigate={navigate}/>
            <div className="flex flex-col min-h-screen gap-1 w-full">
                <Navbar pageName="Dashboard"/>
                <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6 p-6">
                    <StatsCard title="Total Remaining Leaves" value={totalLeaves} description="Across all types this year"/>
                    <StatsCard title="Approved" value={approvedCount} description={`${approvedCount} approved`}/>
                    <StatsCard title="Pending" value={pendingCount} description="Awaiting manager review"/>
                </div>
                <div className="leave-balances p-6 pt-0">
                    <LeaveBalanceCard leaveBalances={balances}/>
                </div>
                <div className="leave-table p-6 pt-0">
                    <LeaveRequestsTable leaveRequests={leaveRequests} idPresent ={idPresent} reasonPresent ={reasonPresent} appliedOnPresent ={appliedOnPresent} actionPresent ={actionPresent} />
                </div>
            </div>
        
        </div>
    )
}
