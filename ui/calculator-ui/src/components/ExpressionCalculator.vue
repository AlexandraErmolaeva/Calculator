<template>
  <div class="calculator">
    <h2>Рассчитать выражение</h2>

    <input
      v-model="expressionInput" placeholder="Введите математическое выражение" title="Например: sqrt(64)*8/92^2"
      @input="clearError"
    />

    <button @click="calculate">Рассчитать</button>

    <p v-if="result !== null">Результат: {{ result }}</p>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script>
import api from "../api";

export default {
  data() {
    return {
      expressionInput: "", 
      result: null,
      errorMessage: "",
    };
  },
  methods: {
    clearError() {
      this.errorMessage = "";
    },

    async calculate() {
      if (!this.expressionInput) {
        this.errorMessage = "Введите выражение";
        return;
      }

      try {
        const response = await api.post("/calculator/expr", {
          expression: this.expressionInput,
        });
        this.result = response.data.result; 
      } catch (error) {
        this.errorMessage = "Ошибка при вычислении";
      }
    },
  },
};
</script>

<style scoped>
.calculator {
  text-align: center;
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border-radius: 10px;
  background-color: #f9f9f9;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
}

h2 {
  font-size: 24px;
  margin-bottom: 20px;
}

input, button {
  display: block;
  margin: 10px auto;
  padding: 12px;
  width: 100%;
  max-width: 320px;
  border-radius: 8px;
  border: 1px solid #ccc;
  font-size: 16px;
  background-color: #fff;
  transition: all 0.3s ease;
}

input:focus, button:hover {
  border-color: #5ea5c8;
  outline: none;
}

button {
  background-color: #2799d1;
  color: white;
  cursor: pointer;
  border: none;
  font-weight: bold;
}

button:hover {
  background-color: #5ea5c8;
}

.error {
  color: red;
  margin-top: 10px;
  font-size: 14px;
}

input[title]:hover {
  border: 1px solid #23bad4;
}
</style>
