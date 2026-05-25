#!/bin/bash

# Файлы
PARAMS_FILE="params.txt"
BLOCKMESH="system/blockMeshDict"

# Проверка наличия файлов
if [ ! -f "$PARAMS_FILE" ]; then
    echo "Ошибка: $PARAMS_FILE не найден"
    exit 1
fi

if [ ! -f "$BLOCKMESH" ]; then
    echo "Ошибка: $BLOCKMESH не найден"
    exit 1
fi

# Функция обновления переменной в blockMeshDict
update_var() {
    local var_name="$1"
    local new_value="$2"
    # Заменяем строку вида "var_name число;" на "var_name новое_число;"
    sed -i -E "s/^(${var_name}[[:space:]]+)[0-9]+;/\1${new_value};/" "$BLOCKMESH"
}

# Чтение params.txt и обновление переменных
# Структура: имя_переменной, затем значение на следующей строке
# Пропускаем U (она не нужна в blockMeshDict)

# Читаем весь файл в массив строк
mapfile -t lines < "$PARAMS_FILE"

# Проходим по парам (имя, значение), начиная с первой строки (U) пропускаем
for (( i=0; i<${#lines[@]}; i+=2 )); do
    var_name="${lines[i]}"
    var_value="${lines[i+1]}"
    # Пропускаем U
    if [[ "$var_name" == "U" ]]; then
        continue
    fi
    # Обновляем переменную в blockMeshDict
    update_var "$var_name" "$var_value"
    echo "Обновлено: $var_name = $var_value"
done

echo "Готово. blockMeshDict обновлён."
