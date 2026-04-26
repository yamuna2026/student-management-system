import { useEffect, useState, useCallback } from "react";
import API from "../services/api";
import Navbar from "../components/Navbar";
import "./AdminDashboard.css";

const AdminDashboard = () => {
  const [students, setStudents] = useState([]);
  const [form, setForm] = useState({
    studentName: "",
    course: ""
  });

  const [updateId, setUpdateId] = useState(null);

  // ✅ FORM MESSAGE (Create/Update)
  const [formMessage, setFormMessage] = useState("");
  const [formType, setFormType] = useState("");

  // ✅ GLOBAL MESSAGE (Delete)
  const [globalMessage, setGlobalMessage] = useState("");

  // 🔹 FORM MESSAGE FUNCTION
  const showMessage = (msg, type = "success") => {
    setFormMessage(msg);
    setFormType(type);

    setTimeout(() => setFormMessage(""), 3000);
  };

  // 🔹 GLOBAL MESSAGE FUNCTION
  const showGlobalMessage = (msg) => {
    setGlobalMessage(msg);

    setTimeout(() => setGlobalMessage(""), 3000);
  };

  // 📥 FETCH STUDENTS
  const fetchStudents = useCallback(async () => {
    try {
      const res = await API.get("/students");
      setStudents(res.data.data);
    } catch {
      showMessage("Error fetching students", "error");
    }
  }, []);

  useEffect(() => {
    fetchStudents();
  }, [fetchStudents]);

  // ➕ CREATE
  const handleCreate = async () => {
    if (!form.studentName || !form.course) {
      showMessage("All fields required", "error");
      return;
    }

    try {
      const res = await API.post("/students", form);
      showMessage(res.data.message, "success");

      setForm({ studentName: "", course: "" });
      fetchStudents();
    } catch (err) {
      showMessage(err.response?.data?.message || "Error", "error");
    }
  };

  // ✏️ UPDATE
  const handleUpdate = async () => {
    try {
      const res = await API.put(`/students/${updateId}`, form);
      showMessage(res.data.message, "success");

      setUpdateId(null);
      setForm({ studentName: "", course: "" });
      fetchStudents();
    } catch (err) {
      showMessage(err.response?.data?.message || "Error", "error");
    }
  };

  // ❌ CANCEL
  const handleCancel = () => {
    setUpdateId(null);
    setForm({ studentName: "", course: "" });
    showMessage("Update cancelled", "error");
  };

  // 🗑 DELETE
  const handleDelete = async (id) => {
    try {
      const res = await API.delete(`/students/${id}`);
      showGlobalMessage(res.data.message); // ✅ bottom message

      fetchStudents();
    } catch (err) {
      showGlobalMessage(err.response?.data?.message || "Error");
    }
  };

  // 📝 EDIT
  const handleEdit = (s) => {
    setUpdateId(s.studentId);
    setForm({
      studentName: s.studentName,
      course: s.course
    });
  };

  return (
    <div className="dashboard">
      <Navbar title="Admin Dashboard" />

      <div className="container">

        {/* 🔹 LEFT FORM */}
        <div className="form-box">
          <h2>{updateId ? "Update Student" : "Create Student"}</h2>

          <input
            placeholder="Student Name"
            value={form.studentName}
            onChange={(e) =>
              setForm({ ...form, studentName: e.target.value })
            }
          />

          <input
            placeholder="Course"
            value={form.course}
            onChange={(e) =>
              setForm({ ...form, course: e.target.value })
            }
          />

          {updateId ? (
            <div className="btn-group">
              <button onClick={handleUpdate} className="primary">
                Update
              </button>
              <button onClick={handleCancel} className="danger">
                Cancel
              </button>
            </div>
          ) : (
            <button onClick={handleCreate} className="primary">
              Create
            </button>
          )}

          {/* ✅ FORM MESSAGE */}
          {formMessage && (
            <div className={`form-message ${formType}`}>
              {formMessage}
            </div>
          )}
        </div>

        {/* 🔹 RIGHT CARDS */}
        <div className="cards">
          {students.map((s) => (
            <div className="card" key={s.studentId}>

              <div className="actions">
                <span onClick={() => handleEdit(s)}>✏️</span>
                <span onClick={() => handleDelete(s.studentId)}>🗑️</span>
              </div>

              <h3>{s.studentName}</h3>
              <p><b>ID:</b> {s.studentId}</p>
              <p><b>Course:</b> {s.course}</p>

            </div>
          ))}
        </div>

      </div>

      {/* ✅ GLOBAL MESSAGE (BOTTOM) */}
      {globalMessage && (
        <div className="global-message">
          {globalMessage}
        </div>
      )}
    </div>
  );
};

export default AdminDashboard;