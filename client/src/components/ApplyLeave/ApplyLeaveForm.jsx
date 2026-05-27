import { useState } from "react";
import toast from "react-hot-toast";

export default function ApplyLeaveForm({leaveTypes, user}) {
    const [leaveTypeId, setLeaveTypeId] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [reason, setReason] = useState("");
    const [loading, setLoading] = useState(false);
    const employeeId = user?.employeeId;

    const handleSubmit = async () => {
        if (!employeeId || !leaveTypeId || !startDate || !endDate) {
            toast.error("Please fill all required fields");
            return;
        }
    
    try {
        setLoading(true);
        const payload = {
            employeeId,
            leaveTypeId,
            startDate,
            endDate,
            reason   
    };   
    
    const token = localStorage.getItem("token");
    const response = await fetch(
        "http://localhost:5155/api/LeaveRequest",
        {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        }
    );
    if (!response.ok) {
            throw new Error("Failed to submit leave request");
        }

    const result = await response.json();

    console.log(result);
    toast.success("Leave request submitted successfully");
    setLeaveTypeId("");
    setStartDate("");
    setEndDate("");
    setReason("");
    } catch (error) {
        console.error(error);
        toast.error("An error occurred while submitting your request");
    } finally {
        setLoading(false);
    }

};
    const handleCancel = () => {

        setLeaveTypeId("");
        setStartDate("");
        setEndDate("");
        setReason("");
    };  
    return(
        <div className="bg-[var(--surface)] flex flex-col gap-2 p-6 bg-[var(--surface)] rounded-lg border border-[var(--border)] w-full lg:w-1/2">
            <div className="text-md font-bold">New Leave Request</div>
            <div className="text-xs text-[var(--ink3)]">Fill in the details below. Your manager will be notified.</div>
            <div className="text-xs text-[var(--ink2)] mt-3">Leave Type</div>
            <select className="w-full text-sm focus:outline-none border-[var(--border)] border focus:border-[var(--accent)] h-7" value={leaveTypeId} onChange={(e)=>setLeaveTypeId(e.target.value ? Number(e.target.value) : "")}>
                <option value="" className="text-sm">Select leave type...</option>
                    {leaveTypes.map((leaveType) => (
                        <option
                            key={leaveType.id}
                            value={leaveType.id}
                        >
                            {leaveType.typeName}
                        </option>
                    ))}
            </select>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-4 text-sm">
                <div className="flex flex-col">
                    <div className="text-xs text-[var(--ink2)] mt-3">Start Date</div>
                    <input type="date" className="w-full text-xs rounded-xs focus:outline-none border-[var(--border)] border focus:border-[var(--accent)] h-7 mt-2 p-1" value={startDate} onChange={(e)=>setStartDate(e.target.value)}></input>
                </div>
                <div className="flex flex-col">
                    <div className="text-xs text-[var(--ink2)] mt-3">End Date</div>
                    <input type="date" className="w-full text-xs rounded-xs focus:outline-none border-[var(--border)] border focus:border-[var(--accent)] h-7 mt-2 p-1" value={endDate} onChange={(e)=>setEndDate(e.target.value)}></input>
                </div>
            </div>
                <div className="flex flex-col">
                    <div className="flex items-center gap-1 mt-3">
                    <div className="text-xs text-[var(--ink2)]">Reason</div>
                    <span className="text-xs text-[var(--ink3)]">(optional)</span>
                    </div>
                    <textarea placeholder="Briefly describe the reason for your leave..." className="w-full align-top text-xs rounded-xs focus:outline-none border-[var(--border)] border focus:border-[var(--accent)] min-h-[120px] mt-2 p-3" value={reason} onChange={(e)=>setReason(e.target.value)}></textarea>
                </div>
                <div className="flex items-center">
                <button
                    className={`submit-btn text-white text-sm px-4 py-1 rounded transition-colors duration-200 cursor-pointer ${
                        loading
                            ? "bg-[var(--ink)] opacity-80 cursor-not-allowed"
                            : "bg-[var(--accent)] hover:bg-[var(--ink)]"
                    }`}
                    onClick={handleSubmit}
                    disabled={loading}
                >
                    {loading ? "Submitting..." : "Submit Request"}
                </button>
                <button
                    className={`cancel-btn bg-[var(--surface)] text-[var(--ink3)] text-sm px-4 py-1 rounded border border-[var(--border)] ml-2 cursor-pointer ${loading ? " opacity-80 cursor-not-allowed" : " hover:bg-[var(--border)]"}`}
                    onClick={handleCancel}
                    disabled={loading}
                >
                    Cancel
                </button>
                </div>
        </div>
    )
}
