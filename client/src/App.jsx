import { BrowserRouter, Routes, Route } from "react-router-dom"
import Login from "./pages/Login"
import Dashboard from "./pages/Dashboard"
import ApplyLeave from "./pages/ApplyLeave"
import MyLeaves from "./pages/MyLeaves"
import AdminPortal from "./pages/AdminPortal"

function App() {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/" element = {<Login/>}></Route>
      <Route path="/dashboard" element = {<Dashboard/>}></Route>
      <Route path="/apply-leave" element = {<ApplyLeave/>}></Route>
      <Route path="/my-leaves" element = {<MyLeaves/>}></Route>
      <Route path="/admin-portal" element = {<AdminPortal/>}></Route>
    </Routes>
    </BrowserRouter>
  )
}

export default App
