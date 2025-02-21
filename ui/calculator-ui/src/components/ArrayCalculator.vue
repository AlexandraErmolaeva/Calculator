<template>
  <div class="calculator">
    <h2>Рассчитать операцию</h2>

    <!-- Для операций, работающих с несколькими числами (сложение, вычитание, умножение, деление) -->
    <div v-if="operation !== 'root' && operation !== 'pow-from-base' && operation !== 'pow'">
      <input v-model="numbers" placeholder="Введите числа через запятую" />
    </div>

    <!-- Для операции извлечения корня -->
    <div v-if="operation === 'root'">
      <input v-model="baseRootNumber" placeholder="Введите основание корня" />
      <input v-model="rootDegree" placeholder="Введите степень корня" />
    </div>

    <!-- Для операции возведения в степень -->
    <div v-if="operation === 'pow'">
      <input v-model="baseValue" placeholder="Введите число" />
      <input v-model="exponent" placeholder="Введите степень" />
    </div>

    <!-- Для операции нахождения степени по основанию -->
    <div v-if="operation === 'pow-from-base'">
      <input v-model="baseNumber" placeholder="Введите основание" />
      <input v-model="number" placeholder="Введите число" />
    </div>

    <!-- Выполнить операцию -->
    <select v-model="operation">
      <option value="add">Сложение</option>
      <option value="subtract">Вычитание</option>
      <option value="multiply">Умножение</option>
      <option value="divide">Деление</option>
      <option value="pow">Возведение в степень</option>
      <option value="root">Извлечение корня</option>
      <option value="pow-from-base">Нахождение степени по основанию</option>
    </select>

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
      // Массив чисел.
      numbers: null,

      // Возведение в степень.
      baseValue: null,
      exponent: null,

      // Извлечение корня.
      baseRootNumber: null, 
      rootDegree: null,

      // Нахождение степени по основанию.
      baseNumber: null,
      number: null,

      operation: "add",
      result: null,
      errorMessage: ""
    };
},

methods: {
  async calculate() {
    this.result = null;
    this.errorMessage = "";

    let body = {};

    // Функция для проверки, что строка является числом
    const isValidNumber = (value) => /^[+-]?\d+(\.\d+)?$/.test(value);

    if (this.operation !== 'root' && this.operation !== 'pow-from-base' && this.operation !== 'pow') {
      if (!this.numbers) {
        this.errorMessage = "Введите корректное выражение";
        return;
      }

      const numbers = this.numbers.split(',').map(num => num.trim());

      if (numbers.some(num => !isValidNumber(num))) {
        this.errorMessage = "Некорректные значения в выражении. Введите только числа.";
        return;
      }

      body.numbers = numbers.map(num => parseFloat(num));
    }

    if (this.operation === 'root') {
      if (!this.baseRootNumber || !isValidNumber(this.baseRootNumber) || !this.rootDegree || !isValidNumber(this.rootDegree)) {
        this.errorMessage = "Введите корректные значения для основания и степени корня. Только числа.";
        return;
      }

      body.baseRootNumber = parseFloat(this.baseRootNumber); 
      body.rootDegree = parseFloat(this.rootDegree); 
    }

    if (this.operation === 'pow') {
      if (!isValidNumber(this.baseValue) || !isValidNumber(this.exponent)) {
        this.errorMessage = "Введите корректные значения для числа и степени. Только числа.";
        return;
      }

      body.baseValue = parseFloat(this.baseValue); 
      body.exponent = parseFloat(this.exponent); 
    }

    if (this.operation === 'pow-from-base') {
      if (!this.baseNumber || !isValidNumber(this.baseNumber) || !this.number || !isValidNumber(this.number)) {
        this.errorMessage = "Введите корректные значения для основания и числа. Только числа.";
        return;
      }

      body.baseNumber = parseFloat(this.baseNumber);
      body.number = parseFloat(this.number);
    }

    try {
      const response = await api.post(`/calculator/${this.operation}`, body);
      this.result = response.data.result;
    } catch (error) {
      this.errorMessage = "Ошибка при вычислении";
    }
  }
}
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

input, select, button {
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

input:focus, select:focus, button:hover {
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

select {
  cursor: pointer;
  background-color: #f5f5f5;
  border-color: #e0e0e0;
}

select:focus {
  border-color: #5ea5c8;
}

input[title]:hover {
  border: 1px solid #23bad4;
}
</style>
