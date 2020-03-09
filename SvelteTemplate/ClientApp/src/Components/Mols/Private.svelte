<script>
  import { onMount, createEventDispatcher } from "svelte";
  import { reglament } from "../../store.js";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import PickerField from "../../Shared/Forms/PickerField.svelte";

  let dispatcher = createEventDispatcher();
  export let mol = {};

  $: valid =
    mol.SURNAME &&
    mol.NAME1 &&
    mol.NAME2 &&
    mol.ADDRESS &&
    mol.DDEPT_ID &&
    mol.POST_ID;

  function save() {
    if ($reglament.alloEdit && valid) dispatcher("save", mol);
  }

  let depts = [];
  let posts = [];

  onMount(async x => {
    let q = await fetch(`/api/mols/depts`);
    depts = await q.json();
    q = await fetch(`/api/mols/posts`);
    posts = await q.json();
  });
</script>


<form>
  <div class="form-row">
    <div class="form-group col-md-4">
      <TextField text="Фамилия" required bind:value={mol.SURNAME} />
    </div>
    <div class="form-group col-md-4">
      <TextField text="Имя" required bind:value={mol.NAME1} />
    </div>
    <div class="form-group col-md-4">
      <TextField text="Отчество" required bind:value={mol.NAME2} />
    </div>
  </div>


</form>