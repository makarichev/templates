<script>
  import { onMount } from "svelte";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import PickerField from "../../Shared/Forms/PickerField.svelte";
  
  
  export let filter = {search: null};

  let depts = [];
  let posts = [];

  onMount(async x => {
    let q = await fetch(`/api/mols/depts`);
    depts = await q.json();
    q = await fetch(`/api/mols/posts`);
    posts = await q.json();
  });

  $: selectedItem = depts.find(x => x.DEPT_ID == filter.deptId)

</script>


<div class="form-group">
    <TextField text="Поиск" bind:value={filter.search} />
</div>

<div class="form-group">
  <label class="col-form-label col-form-label-sm">Дата:</label>
    <input
      type="date"
      bind:value={filter.date}
      class="form-control"
      placeholder="Дата" />
</div>

<!-- <div class="form-group">
    <PickerField text="Отдел" bind:value={filter.deptId} list={depts}></PickerField>
</div> -->

<div class="form-group">
    <label>Код отдела:</label>
    <input type="search" bind:value={filter.deptId} list="deptsList" class="form-control" />
    <datalist id="deptsList">
      {#each depts as dept}
        <option value="{dept.DEPT_ID}">{dept.NAME}</option>
      {/each}
    </datalist>
    {#if selectedItem}<small class="text-primary">{selectedItem.NAME}</small>{/if}

</div>

<div class="form-group">
    <PickerField text="Должность" bind:value={filter.postId} list={posts}></PickerField>
</div>
