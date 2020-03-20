<script>
    import moment from 'moment'
    export let text = "Поле"
    export let required;
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
  {required}
  value = {innerValue}
  on:change={change}
  type="date"
  class={className}
  id="input{text}" 
  />

