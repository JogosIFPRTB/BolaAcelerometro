# MovimentoBola

Este projeto implementa um controle de bola utilizando física no Unity. O controle pode ser feito tanto por teclado (no Editor e PC) quanto pelo acelerômetro (no Android). Além disso, permite instanciar e destruir objetos ao pressionar um botão.

## 🎮 Funcionalidades
- Controle da bola usando **teclado** (WASD ou setas) no **Editor/Standalone**
- Controle via **acelerômetro** no **Android**
- Instanciação de um objeto ao pressionar um botão
- Destruição automática do objeto após **1 segundo**

## 🛠️ Configuração do Projeto

### 1️⃣ **Requisitos**
- **Unity** 2020.3 ou superior
- **C#**
- **Rigidbody** configurado na bola

### 2️⃣ **Como Usar**
1. Adicione o script `MovimentoBola` a um objeto **esfera** com um `Rigidbody`.
2. No Inspector, defina um **Prefab** para ser instanciado ao pressionar o botão de ação.
3. Configure a velocidade no **Inspector**.
4. Execute o jogo e use:
   - **PC**: Teclas **WASD** ou **setas** para movimentar a bola.
   - **Android**: Movimente o dispositivo para controlar a bola.
   - Pressione **botão de ação** (`Fire1`) para instanciar um objeto temporário.

## 🔧 Estrutura do Código

### `MovimentoBola.cs`
O script principal:
- Captura entrada do usuário (teclado ou acelerômetro)
- Aplica torque à bola com `AddTorque()`
- Instancia e destrói objetos ao pressionar um botão

```csharp
private void FixedUpdate()
{
    AplicarTorque(CapturarEntrada());
}
```

## 📌 Controles
| Plataforma | Movimento | Ação |
|------------|----------|--------|
| PC (Editor/Standalone) | WASD / Setas | Clique esquerdo do mouse |
| Android | Acelerômetro | Toque na tela |

## 🚀 Melhorias Futuras
- Implementar **Object Pooling** para otimizar a criação de objetos.
- Adicionar opção de **ajuste de sensibilidade** para acelerômetro.
- Criar interface gráfica para opções de configuração.

---

Desenvolvido com ❤️ no Unity! 🎮
