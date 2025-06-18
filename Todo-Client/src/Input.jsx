import { useContext, useState } from "react";
import { TodoContext } from "./App";

export const Input = () => {
  const { addItem } = useContext(TodoContext);

  const [value, updateInputValue] = useState("");

  return (
      <input
        type="text"
        value={value}
        onChange={(e) => updateInputValue(e.target.value)}
        onKeyDown={(e) => {
          if (e.key === "Enter") {
            addItem(value);
            updateInputValue("");
          }
        }}
      />
  );
};
