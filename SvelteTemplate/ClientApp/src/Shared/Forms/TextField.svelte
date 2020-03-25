<script>
    export let text = "Поле"
    export let required = false;
    export let className = "form-control"
    export let value = null;
</script>

<label for="input{text}">{text}:</label>
<input
  {required}
  bind:value
  type="search"
  autocomplete="off"
  spellcheck="false"
  class={className}
  id="input{text}" 
  />
