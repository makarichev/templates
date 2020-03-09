<script>
    import moment from 'moment'
    export let text = "Поле"
    export let required = false;
    export let className = "form-control"
    export let value = null;

    $: innerValue = moment(value).format('YYYY-MM-DD')

    const change = (e) => {
        if (!e.target.value) value = null;
        else value = moment(e.target.value).format('YYYY-MM-DDT00:00:00')
    }
</script>

<label for="input{text}">{text}:</label>
<input
  value = {innerValue}
  on:change={change}
  type="date"
  class={className}
  id="input{text}" 
  />

{#if required && !value}
  <small class="text-danger">Обязательно для заполнения</small>
{/if}
