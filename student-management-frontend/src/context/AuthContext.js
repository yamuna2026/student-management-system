import { createContext } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {

  // 🔹 LOGIN
  const login = (token, role) => {
    localStorage.setItem("token", token);
    localStorage.setItem("role", role);
  };

  // 🔹 LOGOUT
  const logout = () => {
    localStorage.clear();
    window.location.href = "/";
  };

  return (
    <AuthContext.Provider value={{ login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};