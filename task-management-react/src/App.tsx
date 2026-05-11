import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import AppRouter from "./routes/AppRouter";

function App() {
  return (
    <>
      <ToastContainer aria-label="Notifications"/>
      <AppRouter />
    </>
  );
}

export default App;