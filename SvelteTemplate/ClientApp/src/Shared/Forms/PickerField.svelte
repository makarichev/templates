<script>
  export let text = "Поле";
  export let required = false;
  export let className = "form-control";
  export let value = null;
  export let list = [];
  export let itemValue = x => x[Object.keys(x)[0]];
  export let itemText = x => x[Object.keys(x)[1]];

  let selectedText = "";
  $: {
    let item = null;
    if (list) item = list.find(x => itemValue(x) === value);  
    if (item) selectedText = itemText(item); else selectedText = null;
  }

  let changed = e => {
    let item = null;
    if (e.target.value)
        item = list.find(x => itemText(x) === e.target.value);
    
    if (item) value = itemValue(item); else value = null;
  };
</script>

<label for="input{text}">{text}:</label>
<input
  {required}
  on:change={changed}
  value={selectedText}
  type="search"
  list="{text}List"
  class={className}
  placeholder=""
  autocomplete="of"
  spellcheck="false"
  id="input{text}" />

<datalist id="{text}List">
  {#each list as item}
    <option value={itemText(item)}>{itemValue(item)}</option>
  {/each}
</datalist>

{#if value !== null}
<small class="text-muted">{value}</small>
{/if}
