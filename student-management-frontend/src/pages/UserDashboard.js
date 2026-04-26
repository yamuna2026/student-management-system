import { useState } from "react";
import API from "../services/api";
import Navbar from "../components/Navbar";
import "./UserDashboard.css";

const UserDashboard = () => {
  const [studentId, setStudentId] = useState("");
  const [student, setStudent] = useState(null);
  const [message, setMessage] = useState("");

  const showMessage = (msg) => {
    setMessage(msg);
    setTimeout(() => setMessage(""), 3000);
  };

  const handleSearch = async () => {
    if (!studentId) {
      showMessage("Enter Student ID");
      return;
    }

    try {
      const res = await API.get(`/students/${studentId}`);
      setStudent(res.data.data);
    } catch {
      setStudent(null);
      showMessage("Student not found");
    }
  };

  return (
    <div className="user-dashboard">
      <Navbar title="User Dashboard" />

      {message && <div className="toast">{message}</div>}

      <div className="search-box">
        <h2>Find Student</h2>

        <div className="search">
          <input
            type="number"
            placeholder="Enter ID"
            value={studentId}
            onChange={(e) => setStudentId(e.target.value)}
          />
          <button onClick={handleSearch}>Search</button>
        </div>

        {/* ✅ UPDATED CARD FORMAT */}
        {student && (
          <div className="card student-card">
            <p><span>Student Name:</span> {student.studentName}</p>
            <p><span>Student ID:</span> {student.studentId}</p>
            <p><span>Course:</span> {student.course}</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default UserDashboard;