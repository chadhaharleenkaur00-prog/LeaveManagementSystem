export default function LeaveRequestsTable({leaveRequests = [], employeeName = "", idPresent= false, reasonPresent= false, appliedOnPresent= false, actionPresent= false, onStatusUpdated}) {
    const updateLeaveStatus = async (id, status) => {
        try {
            const response = await fetch(`http://localhost:5155/api/LeaveRequest/status`, {
                method: "PUT",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ id, status })
            });

            const result = await response.json();

            if (response.ok && result.success) {
                onStatusUpdated?.(id, status);
                alert(`Leave request ${status.toLowerCase()} successfully!`);
                return;
            }

            alert(result.message || `Failed to ${status.toLowerCase()} leave request.`);
        } catch (error) {
            console.error(`Error updating leave request to ${status}:`, error);
            alert("An error occurred while updating the leave request.");
        }
    };

    const isActioned = (status) => {
        const normalizedStatus = status?.toLowerCase();
        return normalizedStatus === "approved" || normalizedStatus === "rejected";
    };

    return(
        <div className="leave-requests-table mt-6 p-6 bg-[white] rounded-lg border border-[var(--border)] w-full">
            <table className="w-full border-collapse bg-[white] rounded-lg ">
                <thead>
                    <tr className="bg-[white]">
                        {idPresent && (
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">#</th>
                        )}
                        {employeeName && (
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">EMPLOYEE</th>
                        )}
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">LEAVE TYPE</th>
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">FROM</th>
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">TO</th>
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">DAYS</th>
                        {reasonPresent && (
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">REASON</th>
                        )}  
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">STATUS</th>
                        {appliedOnPresent && (
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">APPLIED ON</th>
                        )}
                        {actionPresent && (
                        <th className="text-left p-3 text-xs text-[var(--ink3)]">ACTION</th>
                        )}
                    </tr>
                </thead>
                <tbody>
                    {leaveRequests.map((request) => (
                        <tr key={request.id} className="border-t border-[var(--border)]">
                            {idPresent && (
                                <td className="p-3 text-sm">{request.employeeId}</td>
                            )}
                            {employeeName && (
                                <td className="p-3 text-sm">{request.employee?.name || "-"}</td>
                            )}
                            <td className="p-3 text-sm ">{request.leaveType?.typeName || "-"}</td>
                            <td className="p-3 text-sm">{new Date(request.startDate).toLocaleDateString()}</td>
                            <td className="p-3 text-sm">{new Date(request.endDate).toLocaleDateString()}</td>
                            <td className="p-3 text-sm">{Math.ceil((new Date(request.endDate) - new Date(request.startDate))/ (1000 * 60 * 60 * 24)) + 1}</td>
                            {reasonPresent && (
                                <td className="p-3 text-sm">{request.reason || "-"}</td>
                            )}
                            <td className={`p-3 text-sm font-bold ${request.status?.toLowerCase() === "approved" ? "text-green-600" : request.status?.toLowerCase() === "rejected" ? "text-red-600" : "text-yellow-600"}`}>
                                {request.status}
                            </td>
                            {appliedOnPresent && (
                                <td className="p-3 text-sm">{new Date(request.createdAt).toLocaleDateString()}</td>
                            )}
                            {actionPresent && (
                                <td className="p-3 text-sm">
                                    {isActioned(request.status) ? (
                                        <span className="text-sm font-semibold text-[var(--ink2)]">Actioned</span>
                                    ) : (
                                        <>
                                            <button className="bg-green-500 text-white py-1 px-3 rounded hover:bg-green-600" onClick={() => updateLeaveStatus(request.id, "Approved")}>
                                                Approve
                                            </button>
                                            <button className="bg-red-500 text-white py-1 px-3 rounded hover:bg-red-600 ml-2" onClick={() => updateLeaveStatus(request.id, "Rejected")}>
                                                Reject
                                            </button>
                                        </>
                                    )}
                                </td>
                            )}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}
