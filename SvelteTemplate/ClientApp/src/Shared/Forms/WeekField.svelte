<script>
    import moment from 'moment'
    export let text = "Поле"
    export let required = false;
    export let className = "form-control"
    export let value = null;

    $: innerValue = value? moment(value, 'YYYYWW').format('YYYY-[W]WW'): null

    const change = (e) => {
        innerValue = e.target.value
        if (!e.target.value) value = null;
        else value = moment(e.target.value).format('YYYYWW')
        
    }
</script>


<label for="input{text}">{text}:</label>
<input
  value = {innerValue}
  on:change={change}
  type="week"
  class={className}
  id="input{text}" 
  />

{#if required && !value}
  <small class="text-danger">Обязательно для заполнения</small>
{/if}
